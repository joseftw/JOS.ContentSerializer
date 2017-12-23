using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultXhtmlStringPropertyHandler : PropertyHandler<XhtmlString>
    {
        public override object Handle(object value, PropertyInfo property, IContentData contentData)
        {
            //TODO Fix parsing of images/blocks/links etc so we can provide pretty links.

            var xhtmlString = (XhtmlString)value;
            return xhtmlString.ToHtmlString();
        }
    }
}
