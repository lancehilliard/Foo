using System;

namespace ArbitraryValues.ValueGetters {
    class EnumArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        readonly Type _enumType;

        public EnumArbitraryValueGetter(Random random, Type enumType) : base(random) {
            _enumType = enumType;
        }

        public object Get() {
            var values = Enum.GetValues(_enumType);
            var randomIndex = CreateRandomNumber(values.Length - 1);
            var value = values.GetValue(randomIndex);
            return value;
        }
    }
}