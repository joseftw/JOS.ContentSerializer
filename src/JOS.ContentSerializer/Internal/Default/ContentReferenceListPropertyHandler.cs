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

        public object Handle(IEnumerable<ContentReference> value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, propertyInfo, contentData); // TODO Allow injection of settings
        }

        private IEnumerable<object> Execute(
            IEnumerable<ContentReference> contentReferences,
            PropertyInfo property,
            IContentData contentData)
        {
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
