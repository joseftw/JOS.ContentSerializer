using System;
using System.Reflection;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class UrlPropertyHandler : IPropertyHandler<Url>
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContentSerializerSettings _contentSerializerSettings;
        private const string MailTo = "mailto";

        public UrlPropertyHandler(IUrlHelper urlHelper, IContentSerializerSettings contentSerializerSettings)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(
            Url url,
            PropertyInfo propertyInfo,
            IContentData contentData)
        {
            return Handle(url, propertyInfo, contentData, _contentSerializerSettings);
        }

        public object Handle(
            Url url,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            if (url == null)
            {
                return null;
            }

            if (url.Scheme == MailTo) return url.OriginalString;

            if (url.IsAbsoluteUri)
            {
                if (settings.UrlSettings.UseAbsoluteUrls)
                {
                    return url.OriginalString;
                }

                return url.PathAndQuery;
            }

            return this._urlHelper.ContentUrl(url, settings.UrlSettings);
        }
    }
}
