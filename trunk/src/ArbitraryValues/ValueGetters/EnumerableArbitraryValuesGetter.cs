using System;
using System.Linq;

namespace ArbitraryValues.ValueGetters {
    class EnumerableArbitraryValuesGetter<T> : RandomNumberCreator, IArbitraryValueGetter {
        public EnumerableArbitraryValuesGetter(Random random) : base(random) { }

        public object Get() {
            var randomCount = CreateRandomNumber(20);
            var result = Enumerable.Range(1, randomCount).Select(x => Foo.Get<T>()).ToList();
            return result;
        }
    }
}