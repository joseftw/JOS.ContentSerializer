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
        public static Dictionary<string, object> GetStructuredDictionary(this ContentArea contentArea)
        {
            var groupedContentTypes = contentArea.Items.GroupBy(x => x.GetContent().ContentTypeID);
            var propertyDict = new Dictionary<string, object>();

            foreach (var contentType in groupedContentTypes)
            {
                var contentData = contentType.First().GetContent() as ContentData;
                var contentTypeJsonKey = contentData.GetJsonKey();
                var items = GetContentTypeAsList(contentType);
                propertyDict.Add(contentTypeJsonKey, items);
            }

            return propertyDict;
        }

        public static string ToJson(this ContentArea contentArea)
        {
            var propertiesDict = GetStructuredDictionary(contentArea);
            return JsonConvert.SerializeObject(propertiesDict);
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
