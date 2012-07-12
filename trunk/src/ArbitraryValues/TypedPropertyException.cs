using System;

namespace ArbitraryValues {
    public class TypedPropertyException : Exception {
        public TypedPropertyException(string message)
            : base(message) { }
    }
}