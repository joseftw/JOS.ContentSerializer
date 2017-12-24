using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPageReferencePropertyHandler : IPropertyHandler<PageReference>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;

        public DefaultPageReferencePropertyHandler(IPropertyHandler<ContentReference> contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public object Handle(PageReference value, PropertyInfo property, IContentData contentData)
        {
            return this._contentReferencePropertyHandler.Handle(value, property, contentData);
        }
    }
}
