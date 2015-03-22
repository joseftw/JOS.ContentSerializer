using System.Web.Mvc;
using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;

namespace ContentJson.Extensions
{
    public static class UrlExtensions
    {
        public static string ToPrettyUrl(this Url url)
        {
            var helper = ServiceLocator.Current.GetInstance<UrlHelper>();
            var prettyUrl = helper.ContentUrl(url);
            return prettyUrl;
        }
    }
}
