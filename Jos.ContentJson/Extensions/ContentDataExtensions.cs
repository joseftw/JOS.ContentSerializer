using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public static Dictionary<string, object> GetStructuredDictionary(this ContentData contentData)
        {
            var jsonProperties = contentData.GetJsonProperties();
            var propertyDict = ContentJsonHelper.CreatePropertyDictionary(jsonProperties, contentData);
            return propertyDict;
        }

        public static string ToJson(this ContentData contentData)
        {
            var propertiesDict = GetStructuredDictionary(contentData);
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
            var properties = contentData.GetType().GetProperties().Where(PropertyShouldBeIncluded);
            return properties;
        }

        private static bool PropertyShouldBeIncluded(PropertyInfo property)
        {
            var jsonIgnoreAttribute = Attribute.IsDefined(property, typeof(JsonIgnoreAttribute));

            if (jsonIgnoreAttribute) return false;

            var displayAttribute = Attribute.IsDefined(property, typeof (DisplayAttribute));
            if (displayAttribute) return true;

            var jsonPropertyAttribute = Attribute.IsDefined(property, typeof(JsonPropertyAttribute));
            return jsonPropertyAttribute;
        }
    }
}
