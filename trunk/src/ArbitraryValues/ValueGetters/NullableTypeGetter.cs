using System;

namespace ArbitraryValues.ValueGetters {
    internal interface INullableTypeGetter {
        Type Get(Type type);
    }

    class NullableTypeGetter : INullableTypeGetter {
        // http://stackoverflow.com/a/108122/116895
        public Type Get(Type type) {
            type = Nullable.GetUnderlyingType(type);
            var result = type.IsValueType ? typeof(Nullable<>).MakeGenericType(type) : type;
            return result;
        }
    }
}