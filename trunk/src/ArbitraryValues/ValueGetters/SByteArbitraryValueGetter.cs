using System;

namespace ArbitraryValues.ValueGetters {
    class SByteArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        public SByteArbitraryValueGetter(Random random) : base(random) {}

        public object Get() {
            return CreateRandomNumber(sbyte.MaxValue);
        }
    }
}