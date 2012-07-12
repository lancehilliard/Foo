using System.Reflection;

namespace ArbitraryValues {
    public interface IValueProvider {
        object GetValue(object item);
        void Initialize(PropertyInfo property);
    }
}