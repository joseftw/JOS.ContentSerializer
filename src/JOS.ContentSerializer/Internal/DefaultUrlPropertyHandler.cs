using System;
using EPiServer;
using EPiServer.Web;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultUrlPropertyHandler : IUrlPropertyHandler
    {
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;
        private readonly IRequestHostResolver _requestHostResolver;
        private readonly IUrlHelper _urlHelper;

        public DefaultUrlPropertyHandler(
            IUrlHelper urlHelper,
            ISiteDefinitionResolver siteDefinitionResolver,
            IRequestHostResolver requestHostResolver)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _siteDefinitionResolver = siteDefinitionResolver ?? throw new ArgumentNullException(nameof(siteDefinitionResolver));
            _requestHostResolver = requestHostResolver ?? throw new ArgumentNullException(nameof(requestHostResolver));
        }

        public string GetValue(Url url, bool absoluteUrl) // TODO Add settings.
        {
            return Execute(url, absoluteUrl);
        }

        public string GetValue(Url url, string baseUrl, bool absoluteUrl)
        {
            throw new NotImplementedException();
        }

        private string Execute(Url url, bool absoluteUrl)
        {
            if (url.Scheme == "mailto") return url.OriginalString;

            if (url.IsAbsoluteUri)
            {
                if (absoluteUrl)
                {
                    return url.OriginalString;
                }

                return url.PathAndQuery;
            }

            var prettyUrl = this._urlHelper.ContentUrl(url);
            if (absoluteUrl)
            {
                var siteDefinition = this._siteDefinitionResolver.GetByHostname(this._requestHostResolver.HostName, true); // TODO fallback setting
                var uri = new Uri(siteDefinition.SiteUrl, prettyUrl);
                return uri.AbsoluteUri;
            }

            return prettyUrl;
        }
    }
}
