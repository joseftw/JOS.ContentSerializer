using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class XhtmlStringPropertyHandler : IPropertyHandler<XhtmlString>
    {
        public object Handle(XhtmlString value, PropertyInfo property, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            //TODO Fix parsing of images/blocks/links etc so we can provide pretty links.
            return value?.ToHtmlString();
        }
    }
}
