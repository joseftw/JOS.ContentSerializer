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
using Jos.ContentJson.Models.LinkItemCollection;
using Jos.ContentJson.Models.SelectOption;

namespace Jos.ContentJson.Helpers
{
    public class ContentJsonHelper : ContentJsonHelperBase
    {
        public Dictionary<string, object> GetStructuredDictionary(ContentData contentData)
        {
            var jsonProperties = contentData.GetJsonProperties();
            var propertyDict = CreatePropertyDictionary(jsonProperties, contentData);
            return propertyDict;
        }

        private Dictionary<string, object> CreatePropertyDictionary(IEnumerable<PropertyInfo> jsonProperties, ContentData content)
        {
            var propertyDict = new Dictionary<string, object>();

            foreach (var property in jsonProperties)
            {
                var propertyValue = property.GetValue(content, null);

                if (propertyValue is ContentArea) //ContentArea
                {
                    var contentArea = propertyValue as ContentArea;

                    if (contentArea.Items == null || !contentArea.Items.Any()) continue;

                    var contentAreaJsonKey = property.GetJsonKey();
                    var propertyAsDictionary = GetDictionaryFromContentArea(contentArea);

                    propertyDict.Add(contentAreaJsonKey, propertyAsDictionary);
                }
                else if (propertyValue is BlockData) //Internal Block
                {
                    var contentData = propertyValue as ContentData;
                    var blockJsonKey = contentData.GetJsonKey();
                    var blockAsDictionary = GetStructuredDictionary(contentData);

                    propertyDict.Add(blockJsonKey, blockAsDictionary);
                }
                else if (propertyValue is LinkItemCollection)
                {
                    var linkItemCollection = propertyValue as LinkItemCollection;
                    var linksAsDictionary = GetDictionaryFromLinkItemCollection(linkItemCollection, property);

                    if (linksAsDictionary.Any())
                    {
                        propertyDict.Add(linksAsDictionary.First().Key, linksAsDictionary.First().Value);
                    }

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

        private Dictionary<string, object> GetDictionaryFromLinkItemCollection(LinkItemCollection linkItemCollection, PropertyInfo property)
        {
            var links = new List<LinkItemDto>();
            foreach (var link in linkItemCollection)
            {

                var linkItemDto = new LinkItemDto
                {
                    Href = link.Href.StartsWith("mailto:") ? link.Href : link.UrlResolver.Service.GetUrl(link.Href),
                    Language = link.Language,
                    Target = link.Target,
                    Text = link.Text,
                    Title = link.Title,
                };

                links.Add(linkItemDto);
            }

            var jsonKey = property.GetJsonKey();
            return new Dictionary<string, object> { { jsonKey, links } };
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
            var url = casted.ToPrettyUrl();
            var jsonKey = propertyInfo.GetJsonKey();
            return new Dictionary<string, object>{{jsonKey, url}};
        }

        private Dictionary<string, object> GetDictionaryFromContentArea(ContentArea contentArea)
        {
            var groupedContentTypes = contentArea.Items.GroupBy(x => x.GetContent().ContentTypeID);
            var propertyDict = new Dictionary<string, object>();

            foreach (var contentType in groupedContentTypes)
            {
                var contentData = contentType.First().GetContent() as ContentData;
                var contentTypeJsonKey = contentData.GetJsonKey();
                var items = GetContentTypeAsList(contentType);
                propertyDict.Add(contentTypeJsonKey, items);
            }

            return propertyDict;
        }

        private Dictionary<string, object> GetDictionaryFromContentReference(object property, PropertyInfo propertyInfo)
        {
            var contentReference = property as ContentReference;
            var url = contentReference.ToPrettyUrl();
            var jsonKey = propertyInfo.GetJsonKey();
            return new Dictionary<string, object> { { jsonKey, url } };
        }

        private List<object> GetContentTypeAsList(IGrouping<int, ContentAreaItem> contentType)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var items = new List<object>();
            foreach (var item in contentType)
            {
                var loadedItem = contentLoader.Get<ContentData>(item.ContentLink);
                var itemAsDictionary = GetStructuredDictionary(loadedItem);
                items.Add(itemAsDictionary);
            }
            return items;
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