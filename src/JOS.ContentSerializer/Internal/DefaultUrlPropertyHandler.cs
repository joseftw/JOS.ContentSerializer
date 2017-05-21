using System;
using EPiServer;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultUrlPropertyHandler : IUrlPropertyHandler
    {
        private readonly IUrlHelper _urlHelper;

        public DefaultUrlPropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public object GetValue(Url url)
        {
            return Execute(url, new UrlSettings());
        }

        public object GetValue(Url url, UrlSettings urlSettings)
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
