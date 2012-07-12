using System;
using System.Collections.Generic;
using System.Reflection;

namespace ArbitraryValues {
    public interface IBuilderOptions {
        void AddPropertyValueProvider(TypedPropertyKey property, IValueProvider valueProvider);
        bool TryGetValue(object instance, PropertyInfo property, out object value);

        void AddChildLink(Type parentType, Type childType, Type parameterType, string parentProperty, string childProperty);
        bool TryGetSourceLinkDetails(PropertyInfo targetProperty, out ILinkDetails linkDetails);

        void AddInternalLink(Type owningType, Type type, string sourceProperty, string targetProperty);
        void AddDependencies(Type owningType, Type type, string targetProperty, IEnumerable<string> dependencies);

        void AddMap<TSource, TTarget>(Type owningType, string sourcePropertyName, string targetPropertyName, IDictionary<TSource, TTarget> mapping);
        void AddMap<TSource, TTarget>(Type owningType, string sourcePropertyName, string targetPropertyName, Func<TSource, TTarget> modifier);

        void AddMap<TSource1, TSource2, TTarget>(Type owningType, string sourcePropertyName1, string sourcePropertyName2, string targetPropertyName, Func<TSource1, TSource2, TTarget> modifier);

        void AddEnsureRule<TSource, TTarget>(Type owningType, string sourcePropertyName, string targetPropertyName, Func<TSource, TTarget> modifier);

        IEnumerable<ILinkDetails> GetAllLinksWhereSourceIs(PropertyInfo property);
        IEnumerable<ILinkDetails> GetAllLinksWhereSourceIs(string propertyName, Type propertyType, Type owningType);
        IEnumerable<string> GetDependenciesFor(PropertyInfo property);

        void ResetProviders();
    }

    public interface IBuilderOptions<T> : IBuilderOptions {
        void SetConstructionMethod(Func<T> constructionMethod);
        Func<T> ConstructionMethod { get; }
    }
}