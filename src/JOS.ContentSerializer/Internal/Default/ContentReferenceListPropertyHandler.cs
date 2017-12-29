using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentReferenceListPropertyHandler : IPropertyHandler<IEnumerable<ContentReference>>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;

        public ContentReferenceListPropertyHandler(IPropertyHandler<ContentReference> contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public object Handle(IEnumerable<ContentReference> contentReferences, PropertyInfo property, IContentData contentData)
        {
            if (contentReferences == null)
            {
                return null;
            }
            var links = new List<object>();

            foreach (var contentReference in contentReferences)
            {
                var result = this._contentReferencePropertyHandler.Handle(contentReference, property, contentData);
                links.Add(result);
            }

            return links;
        }
    }
}
