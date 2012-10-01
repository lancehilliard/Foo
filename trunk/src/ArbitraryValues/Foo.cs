using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitraryValues.ValueGetters;

namespace ArbitraryValues {
    public class BuilderData {
        public Type OutputType { get; private set; }
        public Func<Random, object> Builder { get; private set; }
        public BuilderData(Type outputType, Func<Random, object> builder) {
            OutputType = outputType;
            Builder = builder;
        }
    }

    public class Foo {
        static readonly List<BuilderData> Builders = new List<BuilderData>();
        static readonly MethodInfo GetMethodInfo;
        static readonly INullableTypeGetter NullableTypeGetter;
        static readonly IStaticNonPublicFieldsGetter StaticNonPublicFieldsGetter;
        static readonly Random Random;

        static Foo() {
            GetMethodInfo = typeof(Foo).GetMethods(BindingFlags.Public | BindingFlags.Static).Single(x => x.Name.Equals("Get") && x.IsGenericMethod && x.GetParameters().SequenceEqual(new List<ParameterInfo>()));
            NullableTypeGetter = new NullableTypeGetter();
            StaticNonPublicFieldsGetter = new StaticNonPublicFieldsGetter();
            Random = new Random();
        }

        static T Get<T>(Type type) {
            T result;
            try {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                    var nullableType = NullableTypeGetter.Get(type);
                    var underlyingType = Nullable.GetUnderlyingType(nullableType);
                    result = Get<T>(underlyingType);
                }
                else {
                    var builderData = Builders.FirstOrDefault(x => x.OutputType.Equals(type));
                    var value = Equals(builderData, default(BuilderData)) ? ArbitraryValueGetter.Get(Random, type) : builderData.Builder(Random);
                    try {
                        result = (T)value;
                    }
                    catch (Exception castException) {
                        try {
                            result = (T)Convert.ChangeType(value, type);
                        }
                        catch (Exception changeTypeException) {
                            throw new Exception(string.Format("ChangeType failed for '{0}' to '{1}'.", value.GetType().FullName, type.FullName));
                        }
                    }
                }
            }
            catch (Exception e) {
                throw new Exception(Messages.FailedToGetValueForType(type), e);
            }
            return result;
        }

        static void AssignArbitraryValues(Type type, String suffix) {
            var fieldInfos = StaticNonPublicFieldsGetter.Get(type).Where(x => x.Name.EndsWith(suffix));
            fieldInfos.ToList().ForEach(x => x.SetValue(null, GetMethodInfo.MakeGenericMethod(x.FieldType).Invoke(null, null)));
        }

        public static void AssignArbitraryValues<T>() {
            AssignArbitraryValues(typeof(T), DefaultNamingScheme.ArbitraryValue);
        }

        public static void AssignArbitraryValues<T>(String suffix) {
            AssignArbitraryValues(typeof(T), suffix);
        }

        //name shouldn't be changed as "GetMethodInfo" depends upon it
        public static T Get<T>() {
            var type = typeof(T);
            var result = Get<T>(type);
            return result;
        }

        public static T GetFrom<T>(IEnumerable<T> enumerable) {
            var result = (T) ArbitraryValueGetter.Get(Random, enumerable);
            return result;
        }

        static void AssignFakes(Type baseType, Func<Type, object> fakeMaker, string suffix) {
            var fieldInfos = StaticNonPublicFieldsGetter.Get(baseType).Where(x => x.Name.EndsWith(suffix));
            foreach (var fieldInfo in fieldInfos) {
                var fake = fakeMaker(fieldInfo.FieldType);
                fieldInfo.SetValue(null, fake);
            }
        }

        static void AssignFakes(Type baseType, Type staticType, Func<Type, Type, object> fakeMaker, string suffix) {
            var fieldInfos = StaticNonPublicFieldsGetter.Get(baseType).Where(x => x.Name.EndsWith(suffix));
            foreach (var fieldInfo in fieldInfos) {
                var fake = fakeMaker(fieldInfo.FieldType, staticType);
                fieldInfo.SetValue(null, fake);
            }
        }

        public static void AssignFakes<T>(Func<Type, object> fakeMaker) {
            AssignFakes(typeof(T), fakeMaker, DefaultNamingScheme.Fake);
        }

        public static void AssignFakes<T>(Func<Type, object> fakeMaker, String suffix) {
            AssignFakes(typeof(T), fakeMaker, suffix);
        }

        public static void AssignFakes<T>(Type staticType, Func<Type, Type, object> fakeMaker) {
            AssignFakes(typeof(T), staticType, fakeMaker, DefaultNamingScheme.Fake);
        }

        public static void AssignFakes<T>(Type staticType, Func<Type, Type, object> fakeMaker, String suffix)
        {
            AssignFakes(typeof(T), staticType, fakeMaker, suffix);
        }

        public static void ClearBuilders() { // todo mlh this is to be called by the user before calls to AddBuilder -- I really want to make this call unnecessary
            Builders.Clear();
        }

        public static void AddBuilder<T>(Func<Random, T> builder) {
            var type = typeof(T);
            if (Builders.Any(x => x.OutputType.Equals(type))) {
                throw new Exception(Messages.BuilderAlreadyAddedForType(type));
            }
            Builders.Add(new BuilderData(type, random => (object)builder(random)));
        }
    }
}