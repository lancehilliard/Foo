using System;

namespace ArbitraryValues.ValueGetters {
    class DateTimeArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        public DateTimeArbitraryValueGetter(Random random) : base(random) { }

        public object Get() {
            return DateTime.Now.AddDays(CreateRandomNumber(short.MaxValue));
        }
    }
}