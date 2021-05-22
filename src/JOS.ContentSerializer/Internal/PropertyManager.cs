using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager : IPropertyManager
    {
        private static readonly ConcurrentDictionary<Type, MethodInfo> CachedHandleMethodInfos;
        private readonly IPropertyResolver _propertyResolver;
        private readonly IPropertyNameStrategy _propertyNameStrategy;
        private readonly IPropertyHandlerService _propertyHandlerService;

        static PropertyManager()
        {
            CachedHandleMethodInfos = new ConcurrentDictionary<Type, MethodInfo>();
        }

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
            IContentSerializerSettings settings)
        {
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

                var method = GetMethodInfo(propertyHandler);

                var key = this._propertyNameStrategy.GetPropertyName(property);
                var value = property.GetValue(contentData);
                var result = method.Invoke(propertyHandler, new[] { value, property, contentData, settings });
                structuredData.Add(key, result);
            }
            return structuredData;
        }

        private static MethodInfo GetMethodInfo(object propertyHandler)
        {
            var type = propertyHandler.GetType();
            if (CachedHandleMethodInfos.ContainsKey(type))
            {
                CachedHandleMethodInfos.TryGetValue(type, out var cachedMethod);
                return cachedMethod;
            }

            var method = propertyHandler.GetType().GetMethods()
                .Where(x => x.Name.Equals(nameof(IPropertyHandler<object>.Handle)))
                .OrderByDescending(x => x.GetParameters().Length)
                .First();

            CachedHandleMethodInfos.TryAdd(type, method);
            return method;
        }
    }
}
