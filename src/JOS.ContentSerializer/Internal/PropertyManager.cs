using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager : IPropertyManager
    {
        private readonly IPropertyResolver _propertyResolver;
        private readonly IPropertyNameStrategy _propertyNameStrategy;
        private readonly IPropertyHandlerService _propertyHandlerService;

        public PropertyManager(
            IPropertyNameStrategy propertyNameStrategy,
            IPropertyResolver propertyResolver,
            IPropertyHandlerService propertyHandlerService
        )
        {
            _propertyNameStrategy = propertyNameStrategy ?? throw new ArgumentNullException(nameof(propertyNameStrategy));
            _propertyResolver = propertyResolver ?? throw new ArgumentNullException(nameof(propertyResolver));
            _propertyHandlerService = propertyHandlerService;
        }

        public Dictionary<string, object> GetStructuredData(
            IContentData contentData,
            ContentSerializerSettings settings)
        {
            var properties = this._propertyResolver.GetProperties(contentData);
            var structuredData = new Dictionary<string, object>();

            foreach (var property in properties)
            {
                var key = this._propertyNameStrategy.GetPropertyName(property);
                var value = property.GetValue(contentData);
                var propertyHandler = this._propertyHandlerService.GetPropertyHandler(property.PropertyType);
                if (propertyHandler == null)
                {
                    Trace.WriteLine($"No PropertyHandler was found for type '{property.PropertyType}'");
                    continue;
                }

                var method = propertyHandler.GetType().GetMethod(nameof(IPropertyHandler<object>.Handle));
                if (method != null)
                {
                    var result = method.Invoke(propertyHandler, new[] { value, property, contentData });
                    AddItem(key, result, structuredData, false);
                }
            }
            return structuredData;
        }

        private static void AddItem(string key, object value, Dictionary<string, object> target, bool throwOnDuplicate)
        {
            if (!target.ContainsKey(key))
            {
                target.Add(key, value);
                return;
            }

            if (throwOnDuplicate)
            {
                throw new ArgumentException("An item with the same key has already been added.");
            }
        }
    }
}
