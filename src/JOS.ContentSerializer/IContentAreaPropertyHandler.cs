using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentAreaPropertyHandler
    {
        object GetValue(ContentArea contentArea);
        object GetValue(ContentArea contentArea, ContentSerializerSettings settings);
    }
}
