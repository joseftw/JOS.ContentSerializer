using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.Shell.ObjectEditing;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;
using Jos.ContentJson.Models.SelectOption;

namespace Jos.ContentJson.Helpers
{
    public class StringPropertyHelper : IPropertyHelper, IStringPropertyHelper
    {
        public string CastProperty(object propertyValue)
        {
            return propertyValue as string;
        }

        public object GetStructuredData(StructuredDataDto dataDto)
        {
            if (dataDto.Property is string && dataDto.PropertyInfo.HasSelectAttribute())
            {
                var selectOneAttribute = dataDto.PropertyInfo.GetSelectOneAttribute();
                Type selectionFactoryType;
                if (selectOneAttribute != null)
                {
                    selectionFactoryType = selectOneAttribute.SelectionFactoryType;
                }
                else
                {
                    var selectManyAttribute = dataDto.PropertyInfo.GetSelectManyAttribute();
                    selectionFactoryType = selectManyAttribute.SelectionFactoryType;
                }

                var valueAsDictionary = GetStructuredData(dataDto.Property, dataDto.PropertyInfo, selectionFactoryType);
                return valueAsDictionary;
            }

            var jsonValue = dataDto.Property;
            return jsonValue;
        }

        private Dictionary<string, object> GetStructuredData(object property, PropertyInfo propertyInfo, Type selectionFactoryType)
        {
            var castedProperty = property as String ?? string.Empty;
            var factoryType = selectionFactoryType;
            var selectOptions = GetSelectionOptions(factoryType, property);

            var jsonKey = propertyInfo.GetJsonKey();
            var items = GetSelectOptions(castedProperty, selectOptions);

            return new Dictionary<string, object> { { jsonKey, items } };
        }

        private IEnumerable<ISelectItem> GetSelectionOptions(Type selectionFactoryType, object property)
        {
            var factory = (ISelectionFactory)Activator.CreateInstance(selectionFactoryType);
            var options = factory.GetSelections(property as ExtendedMetadata);
            return options;
        }

        private IEnumerable<SelectOptionDto> GetSelectOptions(string property, IEnumerable<ISelectItem> selectOptions)
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
