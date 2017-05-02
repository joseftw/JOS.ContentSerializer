using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager
    {
        private readonly IContentAreaPropertyHandler _contentAreaPropertyHandler;
        private readonly IStringPropertyHandler _stringPropertyHandler;
        private readonly IPropertyNameStrategy _propertyNameStrategy;

        public PropertyManager(
            IPropertyNameStrategy propertyNameStrategy,
            IStringPropertyHandler stringPropertyHandler,
            IContentAreaPropertyHandler contentAreaPropertyHandler)
        {
            _propertyNameStrategy = propertyNameStrategy ?? throw new ArgumentNullException(nameof(propertyNameStrategy));
            _stringPropertyHandler = stringPropertyHandler ?? throw new ArgumentNullException(nameof(stringPropertyHandler));
            _contentAreaPropertyHandler = contentAreaPropertyHandler ?? throw new ArgumentNullException(nameof(contentAreaPropertyHandler));
        }

        public Dictionary<string, object> GetStructuredData(
            IContentData contentData,
            IEnumerable<PropertyInfo> properties,
            ContentSerializerSettings settings)
        {
            var structuredData = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                var key = this._propertyNameStrategy.GetPropertyName(property);
                var value = property.GetValue(contentData);
                if (property.PropertyType.IsValueType)
                {
                    structuredData.Add(key, value);
                }

                switch (value)
                {
                    case string _:
                        var stringValue = this._stringPropertyHandler.GetValue(contentData, property);
                        structuredData.Add(key, stringValue);
                        break;
                    case ContentArea c:
                        var contentAreaValue = this._contentAreaPropertyHandler.GetValue(c, settings);
                        structuredData.Add(key, contentAreaValue);
                        break;
                }
            }
            return structuredData;
        }
    }
}
