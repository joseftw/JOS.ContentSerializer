using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace ContentJson.Extensions
{
    public static class ContentReferenceExtensions
    {
        public static string ToPrettyUrl(this ContentReference contentReference)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var pageUrl = urlResolver.GetUrl(contentReference);
            return pageUrl;
        }
    }
}
