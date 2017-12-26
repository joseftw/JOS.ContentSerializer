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

        public UrlPropertyHandler(IUrlHelper urlHelper, IContentSerializerSettings contentSerializerSettings)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(Url value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, this._contentSerializerSettings.UrlSettings);
        }
        
        private string Execute(Url url, IUrlSettings urlSettings)
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
