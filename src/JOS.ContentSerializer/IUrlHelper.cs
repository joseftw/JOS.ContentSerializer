using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IUrlHelper
    {
        string ContentUrl(Url url);
        string ContentUrl(Url url, UrlSettings urlSettings);
        string ContentUrl(ContentReference contentReference);
        string ContentUrl(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings);
    }
}