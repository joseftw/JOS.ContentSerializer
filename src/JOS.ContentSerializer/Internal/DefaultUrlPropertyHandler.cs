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

        public string GetValue(Url url, UrlSettings urlSettings)
        {
            return Execute(url, urlSettings);
        }
        
        private string Execute(Url url, UrlSettings urlSettings)
        {
            if (url.Scheme == "mailto") return url.OriginalString;

            if (url.IsAbsoluteUri)
            {
                if (urlSettings.UseAbsoluteUrls)
                {
                    return url.OriginalString;
                }

                return url.PathAndQuery;
            }

           return this._urlHelper.ContentUrl(url, urlSettings);
        }
    }
}
