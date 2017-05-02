using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentDataPropertyHandler
    {
        object GetValue(ContentData loadedItem);
    }
}
