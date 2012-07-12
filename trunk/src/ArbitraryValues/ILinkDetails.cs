using System;
using System.Reflection;

namespace ArbitraryValues {
    public interface ILinkDetails {
        void SetSourceValue(object value);
        object GetTargetValue();

        bool SourceMatches(PropertyInfo property);
        bool SourceMatches(string propertyName, Type propertyType, Type owningType);

        bool TargetMatches(PropertyInfo targetProperty);
        bool TargetMatches(ILinkDetails linkDetails);
        bool TargetMatches(string propertyName, Type propertyType, Type owningType);
        string TargetPropertyName { get; }
        Type TargetPropertyType { get; }
        Type TargetOwningType { get; }
        bool HasSourceValues { get; }
    }
}