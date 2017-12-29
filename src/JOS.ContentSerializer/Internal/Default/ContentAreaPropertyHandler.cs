using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentAreaPropertyHandler : IPropertyHandler<ContentArea>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPropertyManager _propertyManager;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public ContentAreaPropertyHandler(
            IContentLoader contentLoader,
            IPropertyManager propertyManager,
            IContentSerializerSettings contentSerializerSettings)
        {
            _contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(ContentArea contentArea, PropertyInfo propertyInfo, IContentData contentData)
        {
            if (contentArea == null)
            {
                return null;
            }
            var contentAreaItems = GetContentAreaItems(contentArea);
            if (WrapItems(contentArea, this._contentSerializerSettings))
            {
                var items = new Dictionary<string, List<object>>();
                foreach (var item in contentAreaItems)
                {
                    var result = this._propertyManager.GetStructuredData(item, this._contentSerializerSettings);
                    var typeName = item.GetOriginalType().Name;
                    result.Add(this._contentSerializerSettings.BlockTypePropertyName, typeName);
                    if (items.ContainsKey(typeName))
                    {
                        items[typeName].Add(result);
                    }
                    else
                    {
                        items[typeName] = new List<object> { result };
                    }
                }

                return items;
            }
            else
            {
                var items = new List<object>();
                foreach (var item in contentAreaItems)
                {
                    var result = this._propertyManager.GetStructuredData(item, this._contentSerializerSettings);
                    result.Add(this._contentSerializerSettings.BlockTypePropertyName, item.GetOriginalType().Name);
                    items.Add(result);
                }

                return items;
            }
            
        }

        private IEnumerable<IContentData> GetContentAreaItems(ContentArea contentArea)
        {
            if (contentArea?.FilteredItems == null || !contentArea.FilteredItems.Any())
            {
                return Enumerable.Empty<IContentData>();
            }

            var content = new List<IContentData>();
            foreach (var contentAreaItem in contentArea.FilteredItems)
            {
                var loadedContent = this._contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
                if (loadedContent != null)
                {
                    content.Add(loadedContent);
                }
            }

            return content;
        }

        private static bool WrapItems(ContentArea contentArea, IContentSerializerSettings contentSerializerSettings)
        {
            var wrapItemsAttribute = contentArea.GetType().GetCustomAttribute<ContentSerializerWrapItemsAttribute>();
            var wrapItems = wrapItemsAttribute?.WrapItems ?? contentSerializerSettings.WrapContentAreaItems;
            return wrapItems;
        }
    }
}
