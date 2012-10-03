using System;
using System.Collections.Generic;
using System.Linq;

namespace ArbitraryValues.ValueGetters {
    internal static class ArbitraryValueGetter {
        public static object Get(Random random, Type type) {
            object result;
            IArbitraryValueGetter arbitraryValueGetter = null;

            var typeIsEnumerable = type.GetInterfaces().Any(x => x.Name.StartsWith("IEnumerable"));

            if (type.Name.Equals("Lazy`1")) {
                arbitraryValueGetter = new LazyArbitraryValueGetter(random, type.GetGenericArguments().First());
            }
            else if (type.IsValueType) {
                if (type.IsEnum) {
                    arbitraryValueGetter = new EnumArbitraryValueGetter(random, type);
                }
                else if (typeof(bool).IsAssignableFrom(type)) {
                    arbitraryValueGetter = new BoolArbitraryValueGetter(random);
                }
                else if (typeof(byte).IsAssignableFrom(type)) {
                    arbitraryValueGetter = new ByteArbitraryValueGetter(random);
                }
                else if (typeof(sbyte).IsAssignableFrom(type)) {
                    arbitraryValueGetter = new SByteArbitraryValueGetter(random);
                }
                else if (typeof(DateTime).IsAssignableFrom(type)) {
                    arbitraryValueGetter = new DateTimeArbitraryValueGetter(random);
                }
                else if (typeof(decimal).IsAssignableFrom(type)) {
                    arbitraryValueGetter = new DecimalArbitraryValueGetter(random);
                }
                else {
                    arbitraryValueGetter = new ShortArbitraryValueGetter(random);
                }
            }
            else if (typeof(Uri).IsAssignableFrom(type)) {
                arbitraryValueGetter = new UriArbitraryValueGetter(random);
            }
            else if (typeof(string).IsAssignableFrom(type)) {
                arbitraryValueGetter = new StringArbitraryValueGetter(random);
            }
            else if (typeIsEnumerable) {
                var typeIsDictionary = type.Name.StartsWith("IDictionary");
                var genericArguments = type.GetGenericArguments();
                if (typeIsDictionary) {
                    if (genericArguments.Length == 2) {
                        var keyType = genericArguments[0];
                        var valueType = genericArguments[1];
                        var dictionaryArbitraryValuesGetter = typeof(DictionaryArbitraryValuesGetter<,>);
                        var constructedClass = dictionaryArbitraryValuesGetter.MakeGenericType(new[] { keyType, valueType });
                        arbitraryValueGetter = (IArbitraryValueGetter)Activator.CreateInstance(constructedClass, new object[] { random });
                    }
                    else {
                        throw new Exception(string.Format("Type '{0}' looked like IDictionary, but it had '{1}' generic arguments!", type.FullName, genericArguments.Length));
                    }
                }
                else {
                    if (genericArguments.Length == 1) {
                        var enumerableType = genericArguments[0];
                        var enumerableArbitraryValuesGetter = typeof(EnumerableArbitraryValuesGetter<>);
                        var constructedClass = enumerableArbitraryValuesGetter.MakeGenericType(enumerableType);
                        arbitraryValueGetter = (IArbitraryValueGetter)Activator.CreateInstance(constructedClass, new object[] { random });
                    }
                    else if (genericArguments.Length > 1) {
                        throw new Exception(string.Format("Type '{0}' looked like IEnumerable, but it had '{1}' generic arguments!", type.FullName, genericArguments.Length));
                    }
                }
            }
            if (arbitraryValueGetter == null) {
                arbitraryValueGetter = new ComplexTypeArbitraryValueGetter(random, type);
            }
            try {
                result = arbitraryValueGetter.Get();
            }
            catch (Exception exception) {
                Type childType = null;
                if (type.IsInterface) {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    var types = new List<Type>(); //assemblies.SelectMany(assembly => assembly.GetTypes());
                    foreach (var assembly in assemblies) {
                        try {
                            types.AddRange(assembly.GetTypes());
                        }
                        catch (Exception e) {
                            // todo mlh what to do with this exception?
                        }
                    }
                    childType = types.FirstOrDefault(x => type.IsAssignableFrom(x) && !x.IsAbstract && !x.IsGenericTypeDefinition && !x.IsInterface);
                }
                if (childType == null) {
                    throw new Exception(Messages.NoKnownChildTypes(type), exception);
                }
                result = Get(random, childType);
            }
            return result;
        }

        public static object Get<T>(Random random, IEnumerable<T> enumerable) {
            var result = new EnumerableArbitraryElementGetter(enumerable.Cast<object>().ToArray(), random).Get();
            return result;
        }
    }
}