using System;
using System.Collections.Generic;
using System.Reflection;

namespace ArbitraryValues.ValueGetters
{
    internal interface IStaticNonPublicFieldsGetter
    {
        IEnumerable<FieldInfo> Get(Type type);
    }

    class StaticNonPublicFieldsGetter : IStaticNonPublicFieldsGetter
    {
        public IEnumerable<FieldInfo> Get(Type type)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
        }

    }
}