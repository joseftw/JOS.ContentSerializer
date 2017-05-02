using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentAreaPropertyHandler : IContentAreaPropertyHandler
    {
        private readonly IContentDataPropertyHandler _contentDataPropertyHandler;
        private readonly IContentLoader _contentLoader;

        public DefaultContentAreaPropertyHandler(
            IContentLoader contentLoader,
            IContentDataPropertyHandler contentDataPropertyHandler)
        {
            _contentDataPropertyHandler = contentDataPropertyHandler?? throw new ArgumentNullException(nameof(contentDataPropertyHandler));
            _contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
        }

        public object GetValue(ContentArea contentArea)
        {
            return GetValue(contentArea, null); // TODO Add default settings here.
        }

        public object GetValue(ContentArea contentArea, ContentSerializerSettings settings)
        {
            if (!settings.GlobalWrapContentAreaItems)
            {
                var contentAreaItems = contentArea.Items;
                var propertyList = new List<object>();
                foreach (var contentAreaItem in contentAreaItems)
                {
                    var loadedContentAreaItem = GetLoadedContentAreaItem(contentAreaItem);
                    propertyList.Add(loadedContentAreaItem);
                }

                return propertyList;
            }

            var propertyDict = new Dictionary<string, object>();
            var groupedContentTypes = contentArea.Items.GroupBy(x => x.GetContent().ContentTypeID);

            foreach (var contentType in groupedContentTypes)
            {
                var contentData = (ContentData)contentType.First().GetContent();
                var contentTypeJsonKey = contentData.GetType().Name; // TODO fix this, read from attribute.
                var items = GetContentTypeAsList(contentType);
                propertyDict.Add(contentTypeJsonKey, items);
            }

            return propertyDict;
        }

        public object GetLoadedContentAreaItem(ContentAreaItem contentAreaItem)
        {
            var loadedItem = this._contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
            var itemAsDictionary = this._contentDataPropertyHandler.GetValue(loadedItem);
            return itemAsDictionary;
        }

        private List<object> GetContentTypeAsList(IEnumerable<ContentAreaItem> contentType)
        {
            var items = new List<object>();
            foreach (var item in contentType)
            {
                var loaded = GetLoadedContentAreaItem(item);
                items.Add(loaded);
            }
            return items;
        }
    }
}
