using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentReferencePropertyHandler : IPropertyHandler<ContentReference>
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public ContentReferencePropertyHandler(IUrlHelper urlHelper, IContentSerializerSettings contentSerializerSettings)
        {
            _urlHelper = urlHelper;
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(ContentReference contentReference, PropertyInfo propertyInfo, IContentData contentData)
        {
            if (contentReference == null || contentReference == ContentReference.EmptyReference)
            {
                return null;
            }

            var url = new Uri(this._urlHelper.ContentUrl(contentReference, this._contentSerializerSettings.UrlSettings));

            if (this._contentSerializerSettings.UrlSettings.UseAbsoluteUrls && url.IsAbsoluteUri)
            {
                return url.AbsoluteUri;
            }

            return url.PathAndQuery;
        }
    }
}
