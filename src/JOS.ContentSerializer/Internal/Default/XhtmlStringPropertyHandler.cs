using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class XhtmlStringPropertyHandler : IPropertyHandler<XhtmlString>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public XhtmlStringPropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(
            XhtmlString value,
            PropertyInfo property,
            IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(
            XhtmlString value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            //TODO Fix parsing of images/blocks/links etc so we can provide pretty links.
            return value?.ToHtmlString();
        }
    }
}
