using System;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentReferencePropertyHandler : IContentReferencePropertyHandler
    {
        private readonly IUrlHelper _urlHelper;

        public DefaultContentReferencePropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public object GetValue(ContentReference contentReference)
        {
            return Execute(contentReference, new ContentReferenceSettings());
        }

        public object GetValue(
            ContentReference contentReference,
            ContentReferenceSettings contentReferenceSettings)
        {
            return Execute(contentReference, contentReferenceSettings);
        }

        private object Execute(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings)
        {
            var url = new Uri(this._urlHelper.ContentUrl(contentReference, contentReferenceSettings));

            if (contentReferenceSettings.UseAbsoluteUrls && url.IsAbsoluteUri)
            {
                return url.AbsoluteUri;
            }

            return url.PathAndQuery;
        }
    }
}
