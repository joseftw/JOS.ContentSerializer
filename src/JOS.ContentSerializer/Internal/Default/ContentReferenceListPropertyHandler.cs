using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentReferenceListPropertyHandler : IPropertyHandler<IEnumerable<ContentReference>>
    {
        private readonly IPropertyHandler<ContentReference> _contentReferencePropertyHandler;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public ContentReferenceListPropertyHandler(
            IPropertyHandler<ContentReference> contentReferencePropertyHandler,
            IContentSerializerSettings contentSerializerSettings)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(
            IEnumerable<ContentReference> contentReferences,
            PropertyInfo property,
            IContentData contentData)
        {
            return Handle(contentReferences, property, contentData, _contentSerializerSettings);
        }

        public object Handle(
            IEnumerable<ContentReference> contentReferences,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            if (contentReferences == null)
            {
                return null;
            }
            var links = new List<object>();

            foreach (var contentReference in contentReferences)
            {
                var result = this._contentReferencePropertyHandler.Handle(
                    contentReference,
                    property,
                    contentData,
                    settings);
                links.Add(result);
            }

            return links;
        }
    }
}
