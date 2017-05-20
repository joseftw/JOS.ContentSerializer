using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentReferencePropertyHandler
    {
        string GetValue(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings);
    }
}