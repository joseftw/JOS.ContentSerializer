using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPropertyResolver : IPropertyResolver
    {
        private static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _cachedContentTypes;

        public DefaultPropertyResolver()
        {
            _cachedContentTypes = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();
        }

        public IEnumerable<PropertyInfo> GetProperties(IContentData contentData)
        {
            var type = contentData.GetType();
            if (_cachedContentTypes.ContainsKey(type))
            {
                return _cachedContentTypes[type];
            }

            var properties = type.GetProperties().Where(ShouldBeIncluded).ToList();
            _cachedContentTypes[type] = properties;
            return properties;
        }

        private static bool ShouldBeIncluded(PropertyInfo property)
        {
            var attributes = Attribute.GetCustomAttributes(property);

            var jsonIgnoreAttribute = attributes.OfType<ContentSerializerIgnoreAttribute>().FirstOrDefault();

            if (jsonIgnoreAttribute != null)
            {
                return false;
            }

            var displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute != null)
            {
                return true;
            }

            var jsonPropertyAttribute = attributes.OfType<ContentSerializerIncludeAttribute>().FirstOrDefault();
            return jsonPropertyAttribute != null;
        }
    }
}
