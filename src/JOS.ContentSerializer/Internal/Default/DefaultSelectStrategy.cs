using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;

namespace JOS.ContentSerializer.Internal.Default
{
    public class DefaultSelectStrategy : ISelectOneStrategy, ISelectManyStrategy
    {
        public object Execute(PropertyInfo property, IContentData contentData, ISelectionFactory selectionFactory)
        {
            return GetStructuredData(property, contentData, selectionFactory);
        }

        private static object GetStructuredData(PropertyInfo property, IContentData contentData, ISelectionFactory selectionFactory)
        {
            var selectOptions = GetSelectionOptions(selectionFactory, property);
            var propertyValue = (string)property.GetValue(contentData);
            return GetSelectOptions(propertyValue, selectOptions);
        }

        private static IEnumerable<ISelectItem> GetSelectionOptions(ISelectionFactory selectionFactory, object property)
        {
            var options = selectionFactory.GetSelections(property as ExtendedMetadata);
            return options;
        }

        private static IEnumerable<SelectOption> GetSelectOptions(string property, IEnumerable<ISelectItem> selectOptions)
        {
            var items = new List<SelectOption>();
            var selectedValues = property?.Split(',') ?? Enumerable.Empty<string>();

            foreach (var option in selectOptions)
            {
                var item = new SelectOption
                {
                    Selected = selectedValues.Contains(option.Value.ToString()),
                    Text = option.Text,
                    Value = option.Value.ToString()
                };

                items.Add(item);
            }

            return items;
        }
    }
}
