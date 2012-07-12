using System;

namespace ArbitraryValues.ValueGetters {
    class EnumerableArbitraryElementGetter : RandomElementGetter, IArbitraryValueGetter {
        readonly object[] _enumerable;

        public EnumerableArbitraryElementGetter(object[] enumerable, Random random) : base(random) {
            _enumerable = enumerable;
        }

        public object Get() {
            var result = GetRandomElement(_enumerable);
            return result;
        }
    }
}