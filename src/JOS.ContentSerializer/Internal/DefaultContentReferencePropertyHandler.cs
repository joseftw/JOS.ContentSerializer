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

        public string GetValue(
            ContentReference contentReference,
            ContentReferenceSettings contentReferenceSettings)
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
