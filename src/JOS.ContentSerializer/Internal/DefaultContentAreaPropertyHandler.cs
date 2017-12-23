using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentAreaPropertyHandler : IPropertyHandler<ContentArea>
    {
        private readonly IContentLoader _contentLoader;

        public DefaultContentAreaPropertyHandler(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
        }

        public object Handle(ContentArea contentArea, PropertyInfo propertyInfo, IContentData contentData)
        {
            //var contentAreaItems = this._contentAreaPropertyHandler.GetValue(c, settings);
            //if (WrapItems(c, settings))
            //{
            //    var items = new Dictionary<string, List<object>>();
            //    foreach (var item in contentAreaItems)
            //    {
            //        var result = GetStructuredData((IContentData)item, settings);
            //        var typeName = item.GetOriginalType().Name;
            //        if (items.ContainsKey(typeName))
            //        {
            //            items[typeName].Add(result);
            //        }
            //        else
            //        {
            //            items[typeName] = new List<object> { result };
            //        }
            //    }
            //    AddItem(key, items, structuredData, settings.ThrowOnDuplicate);
            //}
            //else
            //{
            //    var items = new List<object>();
            //    foreach (var item in contentAreaItems)
            //    {
            //        var result = GetStructuredData((IContentData)item, settings);
            //        items.Add(result);
            //    }

            //    AddItem(key, items, structuredData, settings.ThrowOnDuplicate);
            //}
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

        //private static bool WrapItems(ContentArea contentArea, ContentSerializerSettings contentSerializerSettings)
        //{
        //    var wrapItemsAttribute = contentArea.GetType().GetCustomAttribute<ContentSerializerWrapItemsAttribute>(false);
        //    var wrapItems = wrapItemsAttribute?.WrapItems ?? contentSerializerSettings.GlobalWrapContentAreaItems;
        //    return wrapItems;
        //}
    }
}
