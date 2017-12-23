using System;
using System.Reflection;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultUrlPropertyHandler : IPropertyHandler<Url>
    {
        private readonly IUrlHelper _urlHelper;

        public DefaultUrlPropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public object Handle(Url value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, new UrlSettings()); // TODO Allow injection of settings
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
