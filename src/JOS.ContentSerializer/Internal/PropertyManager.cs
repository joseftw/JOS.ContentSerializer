using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager
    {
        private readonly IPropertyNameStrategy _propertyNameStrategy;

        public PropertyManager(IPropertyNameStrategy propertyNameStrategy)
        {
            _propertyNameStrategy = propertyNameStrategy ?? throw new ArgumentNullException(nameof(propertyNameStrategy));
        }

        public Dictionary<string, object> GetStructuredData(
            IContentData contentData,
            IEnumerable<PropertyInfo> properties,
            ContentSerializerSettings settings)
        {
            var structuredData = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsValueType)
                {
                    var key = this._propertyNameStrategy.GetPropertyName(property);
                    var value = property.GetValue(contentData);
                    structuredData.Add(key, value);
                }
            }
            return structuredData;
        }
    }
}
