using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultXhtmlStringPropertyHandler : IXhtmlStringPropertyHandler
    {
        public object GetValue(XhtmlString xhtmlString)
        {
            //TODO Fix parsing of images/blocks/links etc so we can provide pretty links.
            return xhtmlString.ToHtmlString();
        }
    }
}
