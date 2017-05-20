using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.SpecializedProperties;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyManager : IPropertyManager
    {
        private readonly ICustomPropertiesHandler _customPropertiesHandler;
        private readonly ILinkItemCollectionPropertyHandler _linkItemCollectionPropertyHandler;
        private readonly IXhtmlStringPropertyHandler _xhtmlStringPropertyHandler;
        private readonly IContentReferenceListPropertyHandler _contentReferenceListPropertyHandler;
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
            IPageTypePropertyHandler pageTypePropertyHandler,
            IContentReferenceListPropertyHandler contentReferenceListPropertyHandler,
            IXhtmlStringPropertyHandler xhtmlStringPropertyHandler,
            ILinkItemCollectionPropertyHandler linkItemCollectionPropertyHandler,
            ICustomPropertiesHandler customPropertiesHandler
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
            _contentReferenceListPropertyHandler = contentReferenceListPropertyHandler ?? throw new ArgumentNullException(nameof(contentReferenceListPropertyHandler));
            _xhtmlStringPropertyHandler = xhtmlStringPropertyHandler ?? throw new ArgumentNullException(nameof(xhtmlStringPropertyHandler));
            _linkItemCollectionPropertyHandler = linkItemCollectionPropertyHandler ?? throw new ArgumentNullException(nameof(linkItemCollectionPropertyHandler));
            _customPropertiesHandler = customPropertiesHandler ?? throw new ArgumentNullException(nameof(customPropertiesHandler));
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
                    continue;
                }

                var value = property.GetValue(contentData);

                switch (value)
                {
                    case string _:
                        var stringValue = this._stringPropertyHandler.GetValue(contentData, property);
                        AddItem(key, stringValue, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case ContentArea c:
                        var contentAreaItems = this._contentAreaPropertyHandler.GetValue(c, settings);
                        if (WrapItems(c, settings))
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
                            AddItem(key, items, structuredData, settings.ThrowOnDuplicate);
                        }
                        else
                        {
                            var items = new List<object>();
                            foreach (var item in contentAreaItems)
                            {
                                var result = GetStructuredData(item, settings);
                                items.Add(result);
                            }
                            
                            AddItem(key, items, structuredData, settings.ThrowOnDuplicate);
                        }
                        break;
                    case Url url:
                        var urlValue = this._urlPropertyHandler.GetValue(url, settings.UrlSettings);
                        AddItem(key, urlValue, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case string[] _:
                        var strings = this._stringArrayPropertyHandler.GetValue(contentData, property);
                        AddItem(key, strings, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case BlockData b:
                        var blockDataResult = GetStructuredData(b, settings);
                        AddItem(key, blockDataResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case ContentReference c:
                        var contentReferenceResult =
                            this._contentReferencePropertyHandler.GetValue(c, settings.ContentReferenceSettings);
                        AddItem(key, contentReferenceResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case PageType pt:
                        var pageTypeResult = this._pageTypePropertyHandler.GetValue(pt);
                        AddItem(key, pageTypeResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case IList<ContentReference> contentReferenceList:
                        var contentReferenceListResult =
                            this._contentReferenceListPropertyHandler.GetValue(contentReferenceList,
                                settings.ContentReferenceSettings);
                        AddItem(key, contentReferenceListResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case XhtmlString x:
                        var xhtmlStringResult = this._xhtmlStringPropertyHandler.GetValue(x);
                        AddItem(key, xhtmlStringResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                    case LinkItemCollection linkItemCollection:
                        var linkItemCollectionResult =
                            this._linkItemCollectionPropertyHandler.GetValue(linkItemCollection, settings.UrlSettings);
                        AddItem(key, linkItemCollectionResult, structuredData, settings.ThrowOnDuplicate);
                        break;
                    default:
                        if (settings.UseCustomPropertiesHandler && value != null)
                        {
                            var customPropertyResult = this._customPropertiesHandler.GetValue(value);
                            if (customPropertyResult != null)
                            {
                                AddItem(key, customPropertyResult, structuredData, settings.ThrowOnDuplicate);
                            }
                        }
                        break;
                }
            }
            return structuredData;
        }

        private static bool WrapItems(ContentArea contentArea, ContentSerializerSettings contentSerializerSettings)
        {
            var wrapItemsAttribute = contentArea.GetType().GetCustomAttribute<WrapItemsAttribute>(false);
            var wrapItems = wrapItemsAttribute?.WrapItems ?? contentSerializerSettings.GlobalWrapContentAreaItems;
            return wrapItems;
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
