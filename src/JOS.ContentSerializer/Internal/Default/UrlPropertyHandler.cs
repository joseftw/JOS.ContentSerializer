using System;
using System.Reflection;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class UrlPropertyHandler : IPropertyHandler<Url>
    {
        private readonly IUrlHelper _urlHelper;
        private const string MailTo = "mailto";

        public UrlPropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public object Handle(Url url, PropertyInfo propertyInfo, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            if (url == null)
            {
                return null;
            }

            if (url.Scheme == MailTo) return url.OriginalString;

            if (url.IsAbsoluteUri)
            {
                if (contentSerializerSettings.UrlSettings.UseAbsoluteUrls)
                {
                    return url.OriginalString;
                }

                return url.PathAndQuery;
            }

            return this._urlHelper.ContentUrl(url, contentSerializerSettings.UrlSettings);
        }
    }
}
