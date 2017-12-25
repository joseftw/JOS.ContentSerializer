using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyResolver : IPropertyResolver
    {
        private static ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _cachedContentTypes;

        public PropertyResolver()
        {
            _cachedContentTypes = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();
        }

        public IEnumerable<PropertyInfo> GetProperties(IContentData contentData)
        {
            var type = contentData.GetOriginalType();
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

            var ignoreAttribute = attributes.OfType<ContentSerializerIgnoreAttribute>().FirstOrDefault();

            if (ignoreAttribute != null)
            {
                return false;
            }

            var displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute != null)
            {
                return true;
            }

            var includeAttribute = attributes.OfType<ContentSerializerIncludeAttribute>().FirstOrDefault();
            return includeAttribute != null;
        }
    }
}
