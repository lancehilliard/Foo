using System;

namespace ArbitraryValues.ValueGetters {
    class BoolArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        public BoolArbitraryValueGetter(Random random) : base(random) {}

        public object Get() {
            return CreateRandomNumber(1).Equals(1);
        }
    }
}