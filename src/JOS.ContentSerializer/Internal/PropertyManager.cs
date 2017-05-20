using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager : IPropertyManager
    {
        private readonly IPageTypePropertyHandler _pageTypePropertyHandler;
        private readonly IContentReferencePropertyHandler _contentReferencePropertyHandler;
        private readonly IStringArrayPropertyHandler _stringArrayPropertyHandler;
        private readonly IValueTypePropertyHandler _valueTypePropertyHandler;
        private readonly IUrlPropertyHandler _urlPropertyHandler;
        private readonly IPropertyResolver _propertyResolver;
        private readonly IContentAreaPropertyHandler _contentAreaPropertyHandler;
        private readonly IStringPropertyHandler _stringPropertyHandler;
        private readonly IPropertyNameStrategy _propertyNameStrategy;

        public PropertyManager(
            IValueTypePropertyHandler valueTypePropertyHandler,
            IPropertyNameStrategy propertyNameStrategy,
            IPropertyResolver propertyResolver,
            IStringPropertyHandler stringPropertyHandler,
            IContentAreaPropertyHandler contentAreaPropertyHandler,
            IUrlPropertyHandler urlPropertyHandler,
            IStringArrayPropertyHandler stringArrayPropertyHandler,
            IContentReferencePropertyHandler contentReferencePropertyHandler,
            IPageTypePropertyHandler pageTypePropertyHandler
        )
        {
            _valueTypePropertyHandler = valueTypePropertyHandler ?? throw new ArgumentNullException(nameof(valueTypePropertyHandler));
            _propertyNameStrategy = propertyNameStrategy ?? throw new ArgumentNullException(nameof(propertyNameStrategy));
            _propertyResolver = propertyResolver ?? throw new ArgumentNullException(nameof(propertyResolver));
            _stringPropertyHandler = stringPropertyHandler ?? throw new ArgumentNullException(nameof(stringPropertyHandler));
            _contentAreaPropertyHandler = contentAreaPropertyHandler ?? throw new ArgumentNullException(nameof(contentAreaPropertyHandler));
            _urlPropertyHandler = urlPropertyHandler ?? throw new ArgumentNullException(nameof(urlPropertyHandler));
            _stringArrayPropertyHandler = stringArrayPropertyHandler ?? throw new ArgumentNullException(nameof(stringArrayPropertyHandler));
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
            _pageTypePropertyHandler = pageTypePropertyHandler ?? throw new ArgumentNullException(nameof(pageTypePropertyHandler));
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
                if (property.PropertyType.IsValueType)
                {
                    structuredData.Add(key, this._valueTypePropertyHandler.GetValue(contentData, property));
                }

                var value = property.GetValue(contentData);

                switch (value)
                {
                    case string _:
                        var stringValue = this._stringPropertyHandler.GetValue(contentData, property);
                        structuredData.Add(key, stringValue);
                        break;
                    case ContentArea c:
                        var contentAreaItems = this._contentAreaPropertyHandler.GetValue(c, settings);
                        if (settings.GlobalWrapContentAreaItems)
                        {
                            var items = new Dictionary<string, List<object>>();
                            foreach (var item in contentAreaItems)
                            {
                                var result = GetStructuredData(item, settings);
                                var typeName = item.GetOriginalType().Name;
                                if (items.ContainsKey(typeName))
                                {
                                    items[typeName].Add(result);
                                }
                                else
                                {
                                    items[typeName] = new List<object> {result};
                                }
                            }
                            structuredData.Add(key, items);
                        }
                        else
                        {
                            var items = new List<object>();
                            foreach (var item in contentAreaItems)
                            {
                                var result = GetStructuredData(item, settings);
                                items.Add(result);
                            }

                            structuredData.Add(key, items);
                        }
                        break;
                    case Url url:
                        var urlValue = this._urlPropertyHandler.GetValue(url, settings.UrlSettings);
                        structuredData.Add(key, urlValue);
                        break;
                    case string[] _:
                        var strings = this._stringArrayPropertyHandler.GetValue(contentData, property);
                        structuredData.Add(key, strings);
                        break;
                    case BlockData b:
                        var blockDataResult = GetStructuredData(b, settings);
                        structuredData.Add(key, blockDataResult);
                        break;
                    case ContentReference c:
                        var contentReferenceResult =
                            this._contentReferencePropertyHandler.GetValue(c, settings.ContentReferenceSettings);
                        structuredData.Add(key, contentReferenceResult);
                        break;
                    case PageType pt:
                        var pageTypeResult = this._pageTypePropertyHandler.GetValue(pt);
                        structuredData.Add(key, pageTypeResult);
                        break;
                }
            }
            return structuredData;
        }
    }
}
