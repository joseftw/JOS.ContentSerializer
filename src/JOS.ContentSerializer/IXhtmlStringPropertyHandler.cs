using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IXhtmlStringPropertyHandler
    {
        object GetValue(XhtmlString xhtmlString);
    }
}
