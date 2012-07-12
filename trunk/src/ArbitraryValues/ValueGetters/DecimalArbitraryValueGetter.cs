using System;

namespace ArbitraryValues.ValueGetters {
    class DecimalArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        static readonly short SmallestNativeValueTypeMaxValue = short.MaxValue;
        public DecimalArbitraryValueGetter(Random random) : base(random) { }

        public object Get() {
            return (decimal)CreateRandomNumber(SmallestNativeValueTypeMaxValue);
        }
    }
}