using System;
using System.Reflection;

namespace ArbitraryValues.ValueGetters {
    class LazyArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        readonly Type _lazyType;

        public LazyArbitraryValueGetter(Random random, Type lazyType) : base(random) {
            _lazyType = lazyType;
        }

        public object Get() {
            var valueFactoryType = typeof (Func<>).MakeGenericType(_lazyType);
            var fooGetMethodInfo = typeof (Foo).GetMethod("Get", BindingFlags.Public | BindingFlags.Static);
            var fooGetGenericMethodInfo = fooGetMethodInfo.MakeGenericMethod(_lazyType);
            var valueFactory = Delegate.CreateDelegate(valueFactoryType, fooGetGenericMethodInfo);
            var @params = new object[]{valueFactory};
            var result = CreateGeneric(typeof (Lazy<>), _lazyType, @params);
            return result;
        }

        static object CreateGeneric(Type generic, Type innerType, params object[] args) {
            var specificType = generic.MakeGenericType(new[] { innerType });
            return Activator.CreateInstance(specificType, args);
        }
    }
}