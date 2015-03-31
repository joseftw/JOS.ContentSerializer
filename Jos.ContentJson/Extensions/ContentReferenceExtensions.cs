using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;

namespace Jos.ContentJson.Extensions
{
    public static class ContentReferenceExtensions
    {
        public static string ToPrettyUrl(this ContentReference contentReference, bool getAbsoluteUrl = false)
        {
            var internalUrl = UrlResolver.Current.GetUrl(contentReference);
            if (!getAbsoluteUrl)
            {
                return internalUrl;   
            }

            var url = new UrlBuilder(internalUrl);
            Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.Encoding.UTF8);

            var friendlyUrl = UriSupport.AbsoluteUrlBySettings(url.ToString());
            return friendlyUrl;
        }
    }
}
