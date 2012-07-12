using System;

namespace ArbitraryValues.ValueGetters {
    class RandomNumberCreator {
        readonly Random _random;
        protected RandomNumberCreator(Random random) {
            _random = random;
        }

        protected int CreateRandomNumber(int maxValue) {
            var result = _random.Next(0, maxValue);
            return result;
        }
    }
}