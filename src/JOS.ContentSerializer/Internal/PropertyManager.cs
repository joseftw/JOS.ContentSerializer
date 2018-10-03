using System;
using System.Collections.Generic;
using System.Diagnostics;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager : IPropertyManager
    {
        private readonly IPropertyResolver _propertyResolver;
        private readonly IPropertyNameStrategy _propertyNameStrategy;
        private readonly IPropertyHandlerService _propertyHandlerService;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public PropertyManager(
            IPropertyNameStrategy propertyNameStrategy,
            IPropertyResolver propertyResolver,
            IPropertyHandlerService propertyHandlerService,
            IContentSerializerSettings contentSerializerSettings
        )
        {
            _propertyNameStrategy = propertyNameStrategy ?? throw new ArgumentNullException(nameof(propertyNameStrategy));
            _propertyResolver = propertyResolver ?? throw new ArgumentNullException(nameof(propertyResolver));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
            _propertyHandlerService = propertyHandlerService;
        }

        public Dictionary<string, object> GetStructuredData(
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            var contentSerializerSettings = settings ?? _contentSerializerSettings;
            var properties = this._propertyResolver.GetProperties(contentData);
            var structuredData = new Dictionary<string, object>();

            foreach (var property in properties)
            {
                var propertyHandler = this._propertyHandlerService.GetPropertyHandler(property);
                if (propertyHandler == null)
                {
                    Trace.WriteLine($"No PropertyHandler was found for type '{property.PropertyType}'");
                    continue;
                }

                var method = propertyHandler.GetType().GetMethod(nameof(IPropertyHandler<object>.Handle));
                if (method != null)
                {
                    var key = this._propertyNameStrategy.GetPropertyName(property);
                    var value = property.GetValue(contentData);

                    var result = method.Invoke(propertyHandler, new[] { value, property, contentData });
                    if(result != null || !contentSerializerSettings.IgnoreEmptyValues)
                    {
                        structuredData.Add(key, result);
                    }
                }
            }
            return structuredData;
        }
    }
}
