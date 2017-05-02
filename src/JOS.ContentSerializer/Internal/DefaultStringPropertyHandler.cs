using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using System.Linq;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultStringPropertyHandler : IStringPropertyHandler
    {
        public object GetValue(IContentData contentData, PropertyInfo property)
        {
            if (HasSelectAttribute(property))
            {
                var selectOneAttribute = GetSelectOneAttribute(property);
                Type selectionFactoryType;
                if (selectOneAttribute != null)
                {
                    selectionFactoryType = selectOneAttribute.SelectionFactoryType;
                }
                else
                {
                    var selectManyAttribute = GetSelectManyAttribute(property);
                    selectionFactoryType = selectManyAttribute.SelectionFactoryType;
                }

                var valueAsDictionary = GetStructuredData(property, contentData, selectionFactoryType);
                return valueAsDictionary;
            }

            var value = property.GetValue(contentData);
            return value;
        }

        private static bool HasSelectAttribute(PropertyInfo property)
        {
            var selectOne = GetSelectOneAttribute(property);
            if (selectOne != null) return true;

            var selectMany = GetSelectManyAttribute(property);
            return selectMany != null;
        }

        private static SelectOneAttribute GetSelectOneAttribute(PropertyInfo propertyInfo)
        {
            var attribute = (SelectOneAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(SelectOneAttribute));
            return attribute;
        }

        private static SelectManyAttribute GetSelectManyAttribute(PropertyInfo propertyInfo)
        {
            var selectMany = (SelectManyAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(SelectManyAttribute));
            return selectMany;
        }

        private static object GetStructuredData(PropertyInfo property, IContentData contentData, Type selectionFactoryType)
        {
            var factoryType = selectionFactoryType;
            var selectOptions = GetSelectionOptions(factoryType, property);
            var propertyValue = (string)property.GetValue(contentData);
            return GetSelectOptions(propertyValue, selectOptions);
        }

        private static IEnumerable<ISelectItem> GetSelectionOptions(Type selectionFactoryType, object property)
        {
            var factory = (ISelectionFactory)Activator.CreateInstance(selectionFactoryType);
            var options = factory.GetSelections(property as ExtendedMetadata);
            return options;
        }

        private static IEnumerable<SelectOptionDto> GetSelectOptions(string property, IEnumerable<ISelectItem> selectOptions)
        {
            var items = new List<SelectOptionDto>();
            var selectedValues = property.Split(',');

            foreach (var option in selectOptions)
            {
                var item = new SelectOptionDto
                {
                    Selected = selectedValues.Contains(option.Value),
                    Text = option.Text,
                    Value = option.Value.ToString()
                };

                items.Add(item);
            }

            return items;
        }
    }
}
