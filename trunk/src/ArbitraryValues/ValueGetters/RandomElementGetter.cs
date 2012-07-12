using System;
using System.Linq;

namespace ArbitraryValues.ValueGetters {
    internal class RandomElementGetter : RandomNumberCreator {
        protected RandomElementGetter(Random random) : base(random) { }
        protected object GetRandomElement(object[] enumerable) {
            var randomIndex = CreateRandomNumber(enumerable.Count() - 1);
            var result = enumerable.ElementAt(randomIndex);
            return result;
        }
    }
}