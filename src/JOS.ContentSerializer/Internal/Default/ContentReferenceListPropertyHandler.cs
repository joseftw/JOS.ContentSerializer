using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentReferenceListPropertyHandler : IPropertyHandler<IEnumerable<ContentReference>>, IPropertyHandler2<IEnumerable<ContentReference>>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;

        public ContentReferenceListPropertyHandler(IPropertyHandler<ContentReference> contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public object Handle(IEnumerable<ContentReference> contentReferences, PropertyInfo property, IContentData contentData)
        {
            return HandleInternal(contentReferences, (contentReference) => this._contentReferencePropertyHandler.Handle(contentReference, property, contentData));
        }

        public object Handle2(IEnumerable<ContentReference> contentReferences, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            // Internal JOS IPropertyHandler implementations always implement IPropertyHandler2 as well.
            return HandleInternal(contentReferences, (contentReference) =>
                ((IPropertyHandler2<ContentReference>)this._contentReferencePropertyHandler).Handle2(contentReference, property, contentData, contentSerializerSettings));
        }

        private object HandleInternal(IEnumerable<ContentReference> contentReferences, Func<ContentReference, object> handle)
        {
            if (contentReferences == null)
            {
                return null;
            }
            var links = new List<object>();

            foreach (var contentReference in contentReferences)
            {
                var result = handle(contentReference);
                links.Add(result);
            }

            return links;
        }
    }
}
