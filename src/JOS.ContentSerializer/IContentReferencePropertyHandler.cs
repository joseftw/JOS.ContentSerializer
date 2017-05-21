using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentReferencePropertyHandler
    {
        object GetValue(ContentReference contentReference);
        object GetValue(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings);
    }
}