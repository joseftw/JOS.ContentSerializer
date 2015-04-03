using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using Jos.ContentJson.Helpers;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class ContentDataExtensions
    {
        private static readonly ContentJsonHelper ContentJsonHelper = new ContentJsonHelper();

        public static string ToJson(this ContentData contentData)
        {
            var propertiesDict = ContentJsonHelper.GetStructuredDictionary(contentData);
            return JsonConvert.SerializeObject(propertiesDict);
        }

        public static string GetJsonKey(this ContentData contentData)
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

            throw new Exception(string.Format("Missing ID on JsonObject attribute on class{0}", contentType.Name));
        }

        public static IEnumerable<PropertyInfo> GetJsonProperties(this ContentData contentData)
        {
            var properties = contentData.GetType().GetProperties().Where(HasJsonPropertyAttribute);
            return properties;
        }

        private static bool HasJsonPropertyAttribute(PropertyInfo property)
        {
            var hasAttribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute));
            return hasAttribute != null;
        }
    }
}
