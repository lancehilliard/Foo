using System;
using System.Collections.Generic;
using System.Linq;

namespace ArbitraryValues.ValueGetters {
    class ArbitraryValuesGetter : RandomNumberCreator {
        public ArbitraryValuesGetter(Random random) : base(random) { }
        public IEnumerable<T> Get<T>() {
            var randomCount = CreateRandomNumber(20);
            var result = Enumerable.Range(1, randomCount).Select(x => Foo.Get<T>());
            return result;
        } 
    }
}