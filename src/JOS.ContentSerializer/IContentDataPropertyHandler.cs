using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentDataPropertyHandler
    {
        object GetValue(IContentData loadedItem);
    }
}
