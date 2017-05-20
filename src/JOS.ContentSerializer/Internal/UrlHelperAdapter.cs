using System;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Web.Mvc.Html;

namespace JOS.ContentSerializer.Internal
{
    public class UrlHelperAdapter : IUrlHelper
    {
        private readonly UrlHelper _urlHelper;

        public UrlHelperAdapter(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public string ContentUrl(Url url)
        {
            return this._urlHelper.ContentUrl(url);
        }
    }
}
