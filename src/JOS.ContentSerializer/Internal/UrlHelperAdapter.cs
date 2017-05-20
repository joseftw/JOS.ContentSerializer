using System;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;

namespace JOS.ContentSerializer.Internal
{
    public class UrlHelperAdapter : IUrlHelper
    {
        private readonly UrlHelper _urlHelper;
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;
        private readonly IRequestHostResolver _requestHostResolver;

        public UrlHelperAdapter(
            UrlHelper urlHelper,
            ISiteDefinitionResolver siteDefinitionResolver,
            IRequestHostResolver requestHostResolver)
        {
            _requestHostResolver = requestHostResolver ?? throw new ArgumentNullException(nameof(requestHostResolver));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _siteDefinitionResolver = siteDefinitionResolver ?? throw new ArgumentNullException(nameof(siteDefinitionResolver));
        }

        public string ContentUrl(Url url)
        {
            return Execute(url, new UrlSettings());
        }

        public string ContentUrl(Url url, UrlSettings urlSettings)
        {
            return Execute(url, urlSettings);
        }

        public string ContentUrl(ContentReference contentReference)
        {
            return Execute(contentReference, new ContentReferenceSettings());
        }

        public string ContentUrl(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings)
        {
            return Execute(contentReference, contentReferenceSettings);
        }

        private string Execute(Url url, UrlSettings urlSettings)
        {
            var prettyUrl = this._urlHelper.ContentUrl(url);
            if (urlSettings.UseAbsoluteUrls)
            {
                return CreateAbsoluteUrl(prettyUrl, urlSettings.FallbackToWildcard);
            }

            return prettyUrl;
        }

        private string Execute(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings)
        {
            var prettyUrl = this._urlHelper.ContentUrl(contentReference);
            if (contentReferenceSettings.UseAbsoluteUrls)
            {
                return CreateAbsoluteUrl(prettyUrl, true);
            }

            return prettyUrl;
        }

        private string CreateAbsoluteUrl(string relativeUrl, bool fallbackToWildcard)
        {
            var siteDefinition = this._siteDefinitionResolver.GetByHostname(
                this._requestHostResolver.HostName,
                fallbackToWildcard
            );
            var uri = new Uri(siteDefinition.SiteUrl, relativeUrl);
            return uri.AbsoluteUri;
        }
    }
}
