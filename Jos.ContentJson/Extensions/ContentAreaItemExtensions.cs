using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace Jos.ContentJson.Extensions
{
    public static class ContentAreaItemExtensions
    {
        public static object GetLoadedContentAreaItem(this ContentAreaItem contentAreaItem, string contentTypeProperty = false)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var loadedItem = contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
            var itemAsDictionary = loadedItem.GetStructuredDictionary();
            if(contentTypeProperty != null)
            {
                itemAsDictionary.Add(contentTypeProperty, contentAreaItem.GetContent().getJsonKey());
            }
            return itemAsDictionary;
        }
    }
}
