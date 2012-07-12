using System;
using System.Globalization;

namespace ArbitraryValues.ValueGetters {
    class StringArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        public StringArbitraryValueGetter(Random random) : base(random) {}

        public object Get() {
            return CreateRandomNumber(int.MaxValue).ToString(CultureInfo.InvariantCulture);
        }
    }
}