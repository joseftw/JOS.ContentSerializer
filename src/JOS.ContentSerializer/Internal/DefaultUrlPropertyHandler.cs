using System;
using System.Reflection;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultUrlPropertyHandler : PropertyHandler<Url>
    {
        private readonly IUrlHelper _urlHelper;

        public DefaultUrlPropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public override object Handle(object value, PropertyInfo propertyInfo, IContentData contentData)
        {
            var url = (Url)value;
            return Execute(url, new UrlSettings()); // TODO Allow injection of settings
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
