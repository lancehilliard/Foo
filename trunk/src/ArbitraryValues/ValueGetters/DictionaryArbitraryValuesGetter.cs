using System;
using System.Collections.Generic;
using System.Linq;

namespace ArbitraryValues.ValueGetters {
    class DictionaryArbitraryValuesGetter<DictKeyType, DictValueType> : RandomNumberCreator, IArbitraryValueGetter {
        public DictionaryArbitraryValuesGetter(Random random) : base(random) { }

        public object Get() {
            var result = new Dictionary<DictKeyType, DictValueType>();
            var randomCount = CreateRandomNumber(20);
            var keys = Enumerable.Range(1, randomCount).Select(x => Foo.Get<DictKeyType>());
            var values = Enumerable.Range(1, randomCount).Select(x => Foo.Get<DictValueType>());
            for (int i = 0; i < randomCount; i++) {
                result.Add(keys.ElementAt(i), values.ElementAt(i));
            }
            return result;
        }
    }
}