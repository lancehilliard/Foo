using System;

namespace ArbitraryValues.ValueGetters {
    class ByteArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        public ByteArbitraryValueGetter(Random random) : base(random) {}

        public object Get() {
            return CreateRandomNumber(byte.MaxValue);
        }
    }
}