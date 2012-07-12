using System;
using System.Diagnostics;
using System.Reflection;

namespace ArbitraryValues {
    [DebuggerDisplay("{Name}, {Type}")]
    public class TypedPropertyKey {
        private readonly string normalisedName;

        public TypedPropertyKey(PropertyInfo property)
            : this(property.PropertyType, property.Name) { }

        public TypedPropertyKey(Type type, string name) {
            if (type == null || String.IsNullOrWhiteSpace(name))
                throw new TypedPropertyException("All parameters are required for a valid TypedPropertyKey");

            Type = type;
            Name = name;
            normalisedName = name.ToUpperInvariant();
        }

        public Type Type { get; private set; }
        public string Name { get; private set; }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var other = (TypedPropertyKey)obj;
            return other.Type == Type && other.normalisedName == normalisedName;
        }

        public override int GetHashCode() {
            return (Type.FullName + normalisedName).GetHashCode();
        }
    }
}