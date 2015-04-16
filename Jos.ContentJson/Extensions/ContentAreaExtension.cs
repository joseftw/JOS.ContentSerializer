using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using Jos.ContentJson.Helpers;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class ContentAreaExtension
    {
        public static string ToJson(this ContentArea contentArea, bool wrapItems = true)
        {
            var structuredData = GetStructuredData(contentArea, wrapItems);
            var json = JsonConvert.SerializeObject(structuredData);
            return json;
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
        public static object GetStructuredData(this ContentArea contentArea, bool wrapItems = true)
        {
            var contentJsonHelper = new ContentJsonHelper();

            if (!wrapItems)
            {
                var contentAreaItems = contentArea.Items;
                var propertyList = new List<object>();
                foreach (var contentAreaItem in contentAreaItems)
                {
                    var loadedContentAreaItem = contentJsonHelper.GetLoadedContentAreaItem(contentAreaItem);
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
                var items = contentJsonHelper.GetContentTypeAsList(contentType);
                propertyDict.Add(contentTypeJsonKey, items);
            }

            return propertyDict;
        }
    }
}
