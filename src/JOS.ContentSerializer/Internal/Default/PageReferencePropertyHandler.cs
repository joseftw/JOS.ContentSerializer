using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class PageReferencePropertyHandler : IPropertyHandler<PageReference>, IPropertyHandler2<PageReference>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;

        public PageReferencePropertyHandler(IPropertyHandler<ContentReference> contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public object Handle(PageReference value, PropertyInfo property, IContentData contentData)
        {
            return this._contentReferencePropertyHandler.Handle(value, property, contentData);
        }

        public object Handle2(PageReference value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            // Internal JOS IPropertyHandler implementations always implement IPropertyHandler2 as well.
            return ((IPropertyHandler2<ContentReference>)this._contentReferencePropertyHandler).Handle2(value, property, contentData, contentSerializerSettings);
        }
    }
}
