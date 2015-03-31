using System.Web.Mvc;
using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;

namespace Jos.ContentJson.Extensions
{
    public static class UrlExtensions
    {
        public static string ToPrettyUrl(this Url url)
        {
            if (url.Scheme == "mailto") return url.OriginalString;

            var helper = ServiceLocator.Current.GetInstance<UrlHelper>();
            var prettyUrl = helper.ContentUrl(url);
            return prettyUrl;
        }
    }
}
