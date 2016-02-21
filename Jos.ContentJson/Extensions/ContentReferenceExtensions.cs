using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Routing;

namespace Jos.ContentJson.Extensions
{
    public static class ContentReferenceExtensions
    {
        public static string ToPrettyUrl(this ContentReference contentReference, bool getAbsoluteUrl = false)
        {
            if (contentReference == null) return string.Empty;

            var internalUrl = UrlResolver.Current.GetUrl(contentReference, ContentLanguage.PreferredCulture.Name, new VirtualPathArguments{ValidateTemplate = false});
            if (!getAbsoluteUrl)
            {
                return internalUrl;   
            }

            var url = new UrlBuilder(internalUrl);
            Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.Encoding.UTF8);

            var friendlyUrl = UriSupport.AbsoluteUrlBySettings(url.ToString());
            return friendlyUrl;
        }

        public static string GetUrl(this ContentReference contentReference)
        {
            var url = contentReference.ToPrettyUrl(false);
            return url;
        }
    }
}
