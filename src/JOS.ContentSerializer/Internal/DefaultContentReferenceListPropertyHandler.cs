using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentReferenceListPropertyHandler : IContentReferenceListPropertyHandler
    {
        private readonly IContentReferencePropertyHandler _contentReferencePropertyHandler;

        public DefaultContentReferenceListPropertyHandler(IContentReferencePropertyHandler contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public IEnumerable<object> GetValue(IEnumerable<ContentReference> contentReferences)
        {
            return Execute(contentReferences, new ContentReferenceSettings());
        }

        public IEnumerable<object> GetValue(
            IEnumerable<ContentReference> contentReferences,
            ContentReferenceSettings contentReferenceSettings)
        {
            return Execute(contentReferences, contentReferenceSettings);
        }

        private IEnumerable<object> Execute(
            IEnumerable<ContentReference> contentReferences,
            ContentReferenceSettings contentReferenceSettings)
        {
            var links = new List<object>();

            foreach (var contentReference in contentReferences)
            {
                var result = this._contentReferencePropertyHandler.GetValue(contentReference, contentReferenceSettings);
                links.Add(result);
            }

            return links;
        }
    }
}
