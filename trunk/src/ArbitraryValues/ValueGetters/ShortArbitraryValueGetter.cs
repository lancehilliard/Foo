using System;

namespace ArbitraryValues.ValueGetters {
    class ShortArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        static readonly short SmallestNativeValueTypeMaxValue = short.MaxValue;
        public ShortArbitraryValueGetter(Random random) : base(random) { }

        public object Get() {
            return CreateRandomNumber(SmallestNativeValueTypeMaxValue);
        }
    }
}