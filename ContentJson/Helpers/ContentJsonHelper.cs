using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ContentJson.Extensions;
using ContentJson.Models.LinkItemCollection;
using EPiServer;
using EPiServer.Core;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using Newtonsoft.Json;

namespace ContentJson.Helpers
{
    public class ContentJsonHelper : ContentJsonHelperBase
    {
        public Dictionary<string, object> GetStructuredDictionary(ContentData contentData)
        {
            var jsonProperties = GetJsonProperties(contentData);
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

                    var contentAreaJsonKey = GetJsonKey(property);
                    var propertyAsDictionary = GetDictionaryFromContentArea(contentArea);

                    propertyDict.Add(contentAreaJsonKey, propertyAsDictionary);
                }
                else if (propertyValue is BlockData) //Internal Block
                {
                    var contentData = propertyValue as ContentData;
                    var blockJsonKey = GetJsonKey(contentData);
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

        private Dictionary<string, object> GetDictionaryFromLinkItemCollection(LinkItemCollection linkItemCollection, PropertyInfo property)
        {
            var links = new List<LinkItemDto>();
            foreach (var link in linkItemCollection)
            {
                
                var linkItemDto = new LinkItemDto
                {
                    Text = link.Text,
                    Title = link.Title,
                    Target = link.Target,
                    Href = link.Href.StartsWith("mailto:") ? link.Href : link.UrlResolver.Service.GetUrl(link.Href)
                };

                links.Add(linkItemDto);
            }

            var jsonKey = GetJsonKey(property);
            return new Dictionary<string, object>{{jsonKey, links}};
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

            var jsonKey = GetJsonKey(propertyInfo);
            var jsonValue = property;

            return new Dictionary<string, object>{{jsonKey, jsonValue}};
        }

        private Dictionary<string, object> GetDictionaryFromUrl(object property, PropertyInfo propertyInfo)
        {
            var casted = property as Url;
            var url = casted.ToPrettyUrl();
            var jsonKey = GetJsonKey(propertyInfo);
            return new Dictionary<string, object>{{jsonKey, url}};
        }

        private Dictionary<string, object> GetDictionaryFromContentArea(ContentArea contentArea)
        {
            var groupedContentTypes = contentArea.Items.GroupBy(x => x.GetContent().ContentTypeID);
            var propertyDict = new Dictionary<string, object>();

            foreach (var contentType in groupedContentTypes)
            {
                var contentTypeJsonKey = GetJsonKey(contentType.First().GetContent() as ContentData);
                var items = GetContentTypeAsList(contentType);
                propertyDict.Add(contentTypeJsonKey, items);
            }

            return propertyDict;
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

        private string GetJsonKey(ContentData contentData)
        {
            var contentType = contentData.GetType();
            var attribute = (JsonObjectAttribute)Attribute.GetCustomAttribute(contentData.GetType(), typeof (JsonObjectAttribute));
            if (attribute == null)
            {
                if (contentType.BaseType != null)
                {
                    return contentType.BaseType.Name.LowerCaseFirstLetter();
                }

                return contentType.Name.LowerCaseFirstLetter();
            }

            var jsonKey = attribute.Id;

            if (!string.IsNullOrWhiteSpace(jsonKey)) return jsonKey;

            throw new Exception(string.Format("Missing ID on JsonObject attribute on class{0}", contentType.Name));
        }

        private string GetJsonKey(PropertyInfo property)
        {
            var jsonAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute));

            return jsonAttribute == null ? property.Name : jsonAttribute.PropertyName.LowerCaseFirstLetter();
        }

        private IEnumerable<PropertyInfo> GetJsonProperties(ContentData contentData)
        {
            var properties = contentData.GetType().GetProperties().Where(HasJsonPropertyAttribute);
            return properties;
        }

        private Dictionary<string, object> GetDictionaryFromContentReference(object property, PropertyInfo propertyInfo)
        {
            var contentReference = property as ContentReference;
            var url = contentReference.ToPrettyUrl();
            var jsonKey = GetJsonKey(propertyInfo);
            return new Dictionary<string, object> {{jsonKey, url}};
        }

        private bool HasJsonPropertyAttribute(PropertyInfo property)
        {
            var hasAttribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute));
            return hasAttribute != null;
        }
    }
}