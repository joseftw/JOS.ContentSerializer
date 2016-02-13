using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace Jos.ContentJson.Extensions
{
    public static class ContentAreaItemExtensions
    {
        public static object GetLoadedContentAreaItem(this ContentAreaItem contentAreaItem)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var loadedItem = contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
            var itemAsDictionary = loadedItem.GetStructuredDictionary();
            return itemAsDictionary;
        }
    }
}
