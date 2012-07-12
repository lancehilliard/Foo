using System;

namespace ArbitraryValues.ValueGetters {
    class UriArbitraryValueGetter : RandomNumberCreator, IArbitraryValueGetter {
        public UriArbitraryValueGetter(Random random) : base(random) {}

        public object Get() {
            var randomNumber = CreateRandomNumber(10000);
            var uriString = string.Format("http://example.com/Foo/{0}", randomNumber);
            var result = new Uri(uriString);
            return result;
        }
    }
}