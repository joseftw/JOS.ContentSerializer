using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class PageReferencePropertyHandler : IPropertyHandler<PageReference>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public PageReferencePropertyHandler(
            IPropertyHandler<ContentReference> contentReferencePropertyHandler,
            IContentSerializerSettings contentSerializerSettings)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(PageReference value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(
            PageReference value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            return this._contentReferencePropertyHandler.Handle(value, property, contentData, settings);
        }
    }
}
