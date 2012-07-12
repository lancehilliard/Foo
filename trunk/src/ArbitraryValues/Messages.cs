using System;

namespace ArbitraryValues {
    public static class Messages {
         public static string NoUsableConstructorFound(Type type) {
             var result = string.Format("No usable constructor found for type: '{0}'", type.FullName);
             return result;
         }

        public static string BuilderAlreadyAddedForType(Type type) {
            var result = string.Format("Builder already added for type '{0}'.", type.FullName);
            return result;
        }

        public static string FailedToGetValueForType(Type type) {
            var result = string.Format("Failed to get value for type '{0}'.", type.FullName);
            return result;
        }

        public static string NoKnownChildTypes(Type type) {
            var result = string.Format("No known child type for type '{0}'.", type.FullName);
            return result;
        }
    }
}