using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Caching;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.Cache;
using Jos.ContentJson.Helpers;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class ContentDataExtensions
    {
        private static readonly ContentJsonHelper ContentJsonHelper = new ContentJsonHelper();

        public static Dictionary<string, object> GetStructuredDictionary(this IContentData contentData)
        {
            var jsonProperties = contentData.GetJsonProperties();
            var propertyDict = ContentJsonHelper.CreatePropertyDictionary(jsonProperties, contentData);
            return propertyDict;
        }

        public static string ToJson(this IContentData contentData)
        {
            var propertiesDict = GetStructuredDictionary(contentData);
            var json = JsonConvert.SerializeObject(propertiesDict);
            return json;
        }

        public static string GetJsonKey(this IContentData contentData)
        {
            var contentType = contentData.GetType();
            var attribute = (JsonObjectAttribute)Attribute.GetCustomAttribute(contentData.GetType(), typeof(JsonObjectAttribute));
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

            throw new Exception($"Missing ID on JsonObject attribute on class {contentType.Name}");
        }

        public static IEnumerable<PropertyInfo> GetJsonProperties(this IContentData contentData)
        {
            var properties = contentData.GetType().GetProperties();
            var includedProperties = properties.Where(x => x.PropertyShouldBeIncluded());
            return includedProperties;
        }
    }
}
