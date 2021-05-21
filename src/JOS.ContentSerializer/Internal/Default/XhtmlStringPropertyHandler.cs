using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class XhtmlStringPropertyHandler : IPropertyHandler<XhtmlString>, IPropertyHandler2<XhtmlString>
    {
        public object Handle(XhtmlString value, PropertyInfo property, IContentData contentData)
        {
            //TODO Fix parsing of images/blocks/links etc so we can provide pretty links.
            return value?.ToHtmlString();
        }

        public object Handle2(XhtmlString value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
