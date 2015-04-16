using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Models.SelectOption;

namespace Jos.ContentJson.Helpers
{
    public class ContentJsonHelper : ContentJsonHelperBase
    {
        public Dictionary<string, object> CreatePropertyDictionary(IEnumerable<PropertyInfo> jsonProperties, ContentData content)
        {
            var propertyDict = new Dictionary<string, object>();

            foreach (var property in jsonProperties)
            {
                var propertyValue = property.GetValue(content, null);
                var jsonKey = property.GetJsonKey();

                if (propertyValue is ContentArea) //ContentArea
                {
                    var contentArea = propertyValue as ContentArea;

                    if (contentArea.Items == null || !contentArea.Items.Any()) continue;

                    var structuredData = contentArea.GetStructuredDictionary();

                    propertyDict.Add(jsonKey, structuredData);
                }
                else if (propertyValue is BlockData) //Internal Block
                {
                    var contentData = propertyValue as ContentData;
                    var blockAsDictionary = contentData.GetStructuredDictionary();

                    propertyDict.Add(jsonKey, blockAsDictionary);
                }
                else if (propertyValue is LinkItemCollection)
                {
                    var linkItemCollection = propertyValue as LinkItemCollection;
                    var structuredData = linkItemCollection.GetStructuredData();

                    propertyDict.Add(jsonKey, structuredData);
                }

                else //Simple properties like strings etc
                {
                    var propertyAsDictionary = GetSimplePropertyValue(propertyValue, property);
                    if (propertyAsDictionary.Any())
                    {
                        propertyDict.Add(propertyAsDictionary.First().Key, propertyAsDictionary.First().Value);   
                    }
                }
            }

            return propertyDict;
        }

        private Dictionary<string, object> GetSimplePropertyValue(object property, PropertyInfo propertyInfo)
        {
            if (property is ContentReference)
            {
                var valueAsDictionary = GetDictionaryFromContentReference(property, propertyInfo);
                return valueAsDictionary;
            }
            if (property is Url)
            {
                var valueAsDictionary = GetDictionaryFromUrl(property, propertyInfo);
                return valueAsDictionary;
            }

            if (property is String && propertyInfo.HasSelectAttribute())
            {
                var selectOneAttribute = propertyInfo.GetSelectOneAttribute();
                Type selectionFactoryType; 
                if (selectOneAttribute != null)
                {
                    selectionFactoryType = selectOneAttribute.SelectionFactoryType;
                }
                else
                {
                    var selectManyAttribute = propertyInfo.GetSelectManyAttribute();
                    selectionFactoryType = selectManyAttribute.SelectionFactoryType;
                }

                var valueAsDictionary = GetDictionaryFromSelectProperty(property, propertyInfo, selectionFactoryType);
                return valueAsDictionary;   
            }

            var jsonKey = propertyInfo.GetJsonKey();
            var jsonValue = property;

            return new Dictionary<string, object>{{jsonKey, jsonValue}};
        }

        private Dictionary<string, object> GetDictionaryFromSelectProperty(object property, PropertyInfo propertyInfo, Type selectionFactoryType)
        {
            var castedProperty = property as String ?? string.Empty;
            var factoryType = selectionFactoryType;
            var selectOptions = GetSelectionOptions(factoryType, property);

            var jsonKey = propertyInfo.GetJsonKey();
            var items = GetSelectOptions(castedProperty, selectOptions);

            return new Dictionary<string, object> { { jsonKey, items } };
        }

        private Dictionary<string, object> GetDictionaryFromUrl(object property, PropertyInfo propertyInfo)
        {
            var casted = property as Url;
            var url = casted.ToPrettyUrl(true);
            var jsonKey = propertyInfo.GetJsonKey();
            return new Dictionary<string, object>{{jsonKey, url}};
        }

        private Dictionary<string, object> GetDictionaryFromContentReference(object property, PropertyInfo propertyInfo)
        {
            var contentReference = property as ContentReference;
            var url = contentReference.ToPrettyUrl(true);
            var jsonKey = propertyInfo.GetJsonKey();
            return new Dictionary<string, object> { { jsonKey, url } };
        }

        public object GetLoadedContentAreaItem(ContentAreaItem contentAreaItem)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var loadedItem = contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
            var itemAsDictionary = loadedItem.GetStructuredDictionary();
            return itemAsDictionary;
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