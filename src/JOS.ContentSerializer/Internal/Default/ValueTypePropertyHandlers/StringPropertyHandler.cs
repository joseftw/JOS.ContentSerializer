using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class StringPropertyHandler : IPropertyHandler<string>
    {
        public object Handle(string stringValue, PropertyInfo property, IContentData contentData)
        {
            var selectAttribute = GetSelectAttribute(property);
            return selectAttribute == null ? stringValue : GetStructuredData(property, contentData, selectAttribute);
        }

        private static Attribute GetSelectAttribute(PropertyInfo property)
        {
            return (Attribute)GetAttribute<SelectOneAttribute>(property) ?? GetAttribute<SelectManyAttribute>(property);
        }

        private static T GetAttribute<T>(PropertyInfo propertyInfo) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(propertyInfo, typeof(T));
        }

        private static object GetStructuredData(PropertyInfo property, IContentData contentData, Attribute selectAttribute)
        {
            var selectionOptions = GetSelectionOptions(selectAttribute, property);
            var propertyValue = (string)property.GetValue(contentData);
            var options = GetSelectOptions(propertyValue, selectionOptions);

            var selectedValuesOnlyAttribute = GetAttribute<ContentSerializerSelectedOptionsOnlyAttribute>(property);
            if (selectedValuesOnlyAttribute == null) return options;

            var selectedOptions = options.Where(option => option.Selected);
            if (!selectedValuesOnlyAttribute.SelectedValueOnly) return selectedOptions;

            var selectedValues = selectedOptions.Select(option => option.Value);
            if (selectAttribute is SelectOneAttribute)
            {
                return selectedValues.FirstOrDefault();
            }

            return selectedValues;
        }

        private static IEnumerable<ISelectItem> GetSelectionOptions(Attribute attribute, object property)
        {
            var selectionFactoryType = (attribute as SelectOneAttribute)?.SelectionFactoryType ?? (attribute as SelectManyAttribute)?.SelectionFactoryType;
            if (selectionFactoryType == null) return Enumerable.Empty<ISelectItem>();

            var factory = (ISelectionFactory)Activator.CreateInstance(selectionFactoryType);
            return factory.GetSelections(property as ExtendedMetadata);
        }

        private static IEnumerable<SelectOption> GetSelectOptions(string property, IEnumerable<ISelectItem> selectOptions)
        {
            var selectedValues = property?.Split(',') ?? Enumerable.Empty<string>().ToArray();

            return selectOptions.Select(option => new SelectOption()
            {
                Selected = selectedValues.Contains(option.Value.ToString()),
                Text = option.Text,
                Value = option.Value.ToString()
            });
        }
    }
}
