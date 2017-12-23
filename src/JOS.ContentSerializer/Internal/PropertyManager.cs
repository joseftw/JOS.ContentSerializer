using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager : IPropertyManager
    {
        private readonly IPropertyResolver _propertyResolver;
        private readonly IPropertyNameStrategy _propertyNameStrategy;

        public PropertyManager(
            IPropertyNameStrategy propertyNameStrategy,
            IPropertyResolver propertyResolver
        )
        {
            _propertyNameStrategy = propertyNameStrategy ?? throw new ArgumentNullException(nameof(propertyNameStrategy));
            _propertyResolver = propertyResolver ?? throw new ArgumentNullException(nameof(propertyResolver));
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

                switch (value)
                {
                    case BlockData b:
                        var blockDataResult = GetStructuredData(b, settings);
                        AddItem(key, blockDataResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                }
            }
            return structuredData;
        }



        private void AddItem(string key, object value, Dictionary<string, object> target, bool throwOnDuplicate)
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
