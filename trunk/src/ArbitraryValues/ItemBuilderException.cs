using System;

namespace ArbitraryValues {
    [Serializable]
    public class ItemBuilderException : Exception {
        public ItemBuilderException(string message)
            : base(message) { }

        public ItemBuilderException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}