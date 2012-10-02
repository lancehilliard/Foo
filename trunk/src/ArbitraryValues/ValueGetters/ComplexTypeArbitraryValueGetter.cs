using System;
using System.Linq;
using System.Reflection;

namespace ArbitraryValues.ValueGetters {
    public class ComplexTypeArbitraryValueGetter : IArbitraryValueGetter {
        readonly Random _random;
        readonly Type _type;

        public ComplexTypeArbitraryValueGetter(Random random, Type type) {
            _random = random;
            _type = type;
        }

        public object Get() {
            var constructors = _type.GetConstructors();
            var constructor = constructors.FirstOrDefault(x => x.GetParameters().Count().Equals(constructors.Min(y => y.GetParameters().Count())));

            if (Equals(constructor, default(ConstructorInfo))) {
                throw new Exception(Messages.NoUsableConstructorFound(_type));
            }

            var constructorParameterValues = constructor.GetParameters().Select(x => ArbitraryValueGetter.Get(_random, x.ParameterType)).ToArray();
            object result;
            try {
                result = Activator.CreateInstance(_type, constructorParameterValues);
            }
            catch (Exception e) {
                throw new Exception("Unable to create instance with constructor types: " + string.Join(",", constructorParameterValues.Select(x => x.GetType())), e);
            }
            _type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanWrite).ToList().ForEach(x => {
                try {
                    x.SetValue(result, ArbitraryValueGetter.Get(_random, x.PropertyType), null);
                }
                catch (Exception e) {
                    // todo mlh what to do when there's a problem setting one of the property values on the newly created object?
                }
            });
            return result;
        }
    }
}