using System;
using System.Web;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Web.Mvc.Html;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultUrlPropertyHandler : IUrlPropertyHandler // TODO fix so this class doesnt use HttpContext at all. Use sitedefinition instead
    {
        private readonly UrlHelper _urlHelper;

        public DefaultUrlPropertyHandler(
            UrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public string GetValue(Url url, bool absoluteUrl) // TODO Add settings.
        {
            return Execute(url, true);
        }

        public string GetValue(Url url, string baseUrl, bool absoluteUrl)
        {
            throw new NotImplementedException();
        }

        private string Execute(Url url, bool absoluteUrl)
        {
            if (url.Scheme == "mailto") return url.OriginalString;

            var prettyUrl = this._urlHelper.ContentUrl(url);

            if (absoluteUrl && !url.IsAbsoluteUri)
            {
                var baseUri = new Uri(GetBaseUrl());
                var uri = new Uri(baseUri, prettyUrl);
                return uri.AbsoluteUri;
            }

            return prettyUrl;
        }


        private static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var host = request.Url.Host;
            var scheme = request.Url.Scheme;
            var port = request.Url.Port;

            if (port == 80 || port == 443)
            {
                var baseUrl = $"{scheme}://{host}";
                return baseUrl;
            }

            return $"{scheme}://{host}:{port}";
        }
    }
}
