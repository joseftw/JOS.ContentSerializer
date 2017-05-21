using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class ContentAreaExtensions
    {
        [Obsolete("Library is deprecated, please use JOS.ContentSerializer instead.")]
        public static string ToJson(this ContentArea contentArea, bool wrapItems = true)
        {
            var structuredData = ToSerializable(contentArea, wrapItems);
            var json = JsonConvert.SerializeObject(structuredData);
            return json;
        }

        [Obsolete("Library is deprecated, please use JOS.ContentSerializer instead.")]
        public static object ToSerializable(this ContentArea contentArea, bool wrapItems = true)
        {
            return GetStructuredData(contentArea, wrapItems);
        }

        /// <summary>
        /// Returns the data in the contentArea either as a Dictionary or a List.
        /// </summary>
        /// <param name="contentArea"></param>
        /// <param name="wrapItems">
        /// If set to true, the items will be wrapped in it's own "namespace", useful when working with different
        /// contenttypes in the contentarea
        /// If false, it will be returned as an list of objects.
        /// </param>
        /// <returns></returns>
        [Obsolete("Library is deprecated, please use JOS.ContentSerializer instead.")]
        public static object GetStructuredData(this ContentArea contentArea, bool wrapItems = true)
        {
            if (!wrapItems)
            {
                var contentAreaItems = contentArea.Items;
                var propertyList = new List<object>();
                foreach (var contentAreaItem in contentAreaItems)
                {
                    var loadedContentAreaItem = contentAreaItem.GetLoadedContentAreaItem();
                    propertyList.Add(loadedContentAreaItem);
                }

                return propertyList;
            }

            var propertyDict = new Dictionary<string, object>();
            var groupedContentTypes = contentArea.Items.GroupBy(x => x.GetContent().ContentTypeID);

            foreach (var contentType in groupedContentTypes)
            {
                var contentData = contentType.First().GetContent() as ContentData;
                var contentTypeJsonKey = contentData.GetJsonKey();
                var items = GetContentTypeAsList(contentType);
                propertyDict.Add(contentTypeJsonKey, items);
            }

            return propertyDict;
        }
    
        private static List<object> GetContentTypeAsList(IEnumerable<ContentAreaItem> contentType)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var items = new List<object>();
            foreach (var item in contentType)
            {
                var loadedItem = contentLoader.Get<ContentData>(item.ContentLink);
                var itemAsDictionary = loadedItem.GetStructuredDictionary();
                items.Add(itemAsDictionary);
            }
            return items;
        }
    }
}
