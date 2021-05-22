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
            return Handle(contentReference, propertyInfo, contentData, _contentSerializerSettings);
        }

        public object Handle(
            ContentReference contentReference,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            if (contentReference == null || contentReference == ContentReference.EmptyReference)
            {
                return null;
            }

            var url = new Uri(this._urlHelper.ContentUrl(contentReference, settings.UrlSettings));

            if (settings.UrlSettings.UseAbsoluteUrls && url.IsAbsoluteUri)
            {
                return url.AbsoluteUri;
            }

            return url.PathAndQuery;
        }
    }
}
