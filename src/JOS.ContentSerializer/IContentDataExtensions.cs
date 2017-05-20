using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace JOS.ContentSerializer
{
    public static class IContentDataExtensions
    {
        public static string Serialize(this IContentData contentData)
        {
            var contentSerializer = ServiceLocator.Current.GetInstance<IContentSerializer>();
            return contentSerializer.Serialize(contentData);
        }

        public static string Serialize(this IContentData contentData, ContentSerializerSettings contentSerializerSettings)
        {
            var contentSerializer = ServiceLocator.Current.GetInstance<IContentSerializer>();
            return contentSerializer.Serialize(contentData, contentSerializerSettings);
        }
    }
}
