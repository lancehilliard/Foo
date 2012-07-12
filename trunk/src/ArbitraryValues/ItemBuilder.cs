using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ArbitraryValues {
    internal class ItemBuilder<T> {
        private readonly IBuilderOptions<T> builderOptions;
        readonly Dictionary<TypedPropertyKey, List<Type>> parentTypes = new Dictionary<TypedPropertyKey, List<Type>>();

        public ItemBuilder(IBuilderOptions<T> builderOptions) {
            this.builderOptions = builderOptions;

            builderOptions.ResetProviders();
        }

        public T Build() {
            var instance = (T)CreateInstance(typeof(T));

            var npc = instance as INotifyPropertyChanged;
            if (npc != null)
                npc.PropertyChanged -= NoOp;

            return instance;
        }


        // TODO: Clean this method up
        private void PopulatePropertyValues(object item) {
            var npc = item as INotifyPropertyChanged;
            if (npc != null)
                npc.PropertyChanged += NoOp;

            var itemType = item.GetType();

            var propertiesToBuild = new List<PropertyInfo>();
            var unresolvedProperties = GetPropertiesInDependencyOrder(itemType);

            while (unresolvedProperties.Any()) {
                var unresolvedCount = unresolvedProperties.Count();

                var propertiesResolvedInIteration = new List<string>();
                var unresolvableProperties = new List<string>();

                foreach (var property in unresolvedProperties) {

                    object propertyValue;

                    ILinkDetails linkDetails;
                    if (builderOptions.TryGetSourceLinkDetails(property, out linkDetails)) {
                        if (linkDetails.HasSourceValues)
                            propertyValue = linkDetails.GetTargetValue();
                        else
                            continue;
                    }
                    else {
                        if (!builderOptions.TryGetValue(item, property, out propertyValue))
                            propertiesToBuild.Add(property);
                    }

                    // TODO: Handle this somehow (e.g. create items to populate collections)
                    if (property.GetIndexParameters().Any()) {
                        unresolvableProperties.Add(property.Name);
                        continue;
                    }

                    foreach (var link in builderOptions.GetAllLinksWhereSourceIs(property))
                        link.SetSourceValue(propertyValue);

                    property.SetValue(item, propertyValue, null);
                    propertiesResolvedInIteration.Add(property.Name);
                }

                unresolvedProperties = unresolvedProperties.Where(p => !propertiesResolvedInIteration.Contains(p.Name) && !unresolvableProperties.Contains(p.Name));

                if (unresolvedCount == unresolvedProperties.Count())
                    break;
            }
            foreach (var property in propertiesToBuild) {
                var propertyKey = new TypedPropertyKey(property);
                List<Type> typesInHierarchy;
                if (parentTypes.TryGetValue(propertyKey, out typesInHierarchy)) {
                    // If the property would cause infinite recursion then don't populate
                    if (typesInHierarchy.Contains(itemType))
                        continue;

                    typesInHierarchy.Add(itemType);
                }
                else
                    parentTypes.Add(propertyKey, new List<Type>());

                var builtItem = CreateInstance(property.PropertyType);
                property.SetValue(item, builtItem, null);
            }
        }

        private static void NoOp(object sender, PropertyChangedEventArgs e) { }

        private readonly Dictionary<Type, IEnumerable<PropertyInfo>> allOrderedProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        private IEnumerable<PropertyInfo> GetPropertiesInDependencyOrder(Type itemType) {
            IEnumerable<PropertyInfo> existingProperties;
            if (allOrderedProperties.TryGetValue(itemType, out existingProperties))
                return new List<PropertyInfo>(existingProperties);

            var orderedProperties = new List<PropertyInfo>();
            var properties = itemType.GetProperties().Where(p => p.CanWrite).ToList();

            bool propertyAdded;
            do {
                propertyAdded = false;
                foreach (var property in properties.Except(orderedProperties)) {
                    PropertyInfo propertyToAdd = null;

                    // Expression dependencies
                    var dependencies = builderOptions.GetDependenciesFor(property);
                    if (!dependencies.Any() || dependencies.All(x => orderedProperties.Select(y => y.Name).Contains(x))) {
                        propertyToAdd = property;
                    }

                    // Explicitly linked Dependencies
                    ILinkDetails linkDetails;
                    if (builderOptions.TryGetSourceLinkDetails(property, out linkDetails)) {
                        if (orderedProperties.Any(p => linkDetails.SourceMatches(p)))
                            propertyToAdd = property;
                    }

                    if (propertyToAdd != null) {
                        orderedProperties.Add(propertyToAdd);
                        propertyAdded = true;
                    }
                }
            } while (propertyAdded);

            if (orderedProperties.Count != properties.Count)
                throw new InvalidOperationException("Could not determine resolution order of all properties.");

            allOrderedProperties.Add(itemType, orderedProperties);
            return orderedProperties;
        }

        private object CreateInstance(Type itemType) {
            if (builderOptions.ConstructionMethod != null) {
                var constructedItem = builderOptions.ConstructionMethod();
                PopulatePropertyValues(constructedItem);
                return constructedItem;
            }

            if (itemType.IsGenericType && !itemType.ContainsGenericParameters) {
                var item = Activator.CreateInstance(itemType);
                PopulatePropertyValues(item);
                return item;
            }

            if (itemType.IsAbstract || itemType.IsInterface || itemType.IsGenericType)
                return null;

            var constructors = itemType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var leastGreedyConstructor = constructors.OrderBy(c => c.GetParameters().Count()).FirstOrDefault();
            var constructorParameters = leastGreedyConstructor.GetParameters().Select<ParameterInfo, object>(p => null);

            try {
                var item = constructorParameters.Any()
                               ? Activator.CreateInstance(itemType, constructorParameters.ToArray())
                               : Activator.CreateInstance(itemType, leastGreedyConstructor.IsPrivate);
                PopulatePropertyValues(item);
                return item;
            }
            catch (MissingMethodException ex) {
                throw new ItemBuilderException(string.Format("Cannot create item of type '{0}'", itemType.FullName), ex);
            }
        }
    }
}