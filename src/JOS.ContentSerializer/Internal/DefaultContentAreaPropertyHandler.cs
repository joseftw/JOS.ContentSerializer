using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentAreaPropertyHandler : IPropertyHandler<ContentArea>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IPropertyManager _propertyManager;

        public DefaultContentAreaPropertyHandler(IContentLoader contentLoader, IPropertyManager propertyManager)
        {
            _contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
        }

        public object Handle(ContentArea contentArea, PropertyInfo propertyInfo, IContentData contentData)
        {
            var contentAreaItems = GetContentAreaItems(contentArea);
            var settings = new ContentSerializerSettings(); // TODO allow injection of settings
            if (WrapItems(contentArea, settings))
            {
                var items = new Dictionary<string, List<object>>();
                foreach (var item in contentAreaItems)
                {
                    var result = this._propertyManager.GetStructuredData(item, settings);
                    var typeName = item.GetOriginalType().Name;
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
                    var result = this._propertyManager.GetStructuredData(item, settings);
                    items.Add(result);
                }

                return items;
            }
            
        }

        private IEnumerable<IContentData> GetContentAreaItems(ContentArea contentArea)
        {
            if (contentArea?.Items == null || !contentArea.Items.Any())
            {
                return Enumerable.Empty<IContentData>();
            }

            var content = new List<IContentData>();
            foreach (var contentAreaItem in contentArea.Items)
            {
                var loadedContent = this._contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
                if (loadedContent != null)
                {
                    content.Add(loadedContent);
                }
            }

            return content;
        }

        private static bool WrapItems(ContentArea contentArea, ContentSerializerSettings contentSerializerSettings)
        {
            var wrapItemsAttribute = contentArea.GetType().GetCustomAttribute<ContentSerializerWrapItemsAttribute>(false);
            var wrapItems = wrapItemsAttribute?.WrapItems ?? contentSerializerSettings.GlobalWrapContentAreaItems;
            return wrapItems;
        }
    }
}
