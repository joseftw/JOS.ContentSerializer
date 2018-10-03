using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentReferencePropertyHandler : IPropertyHandler<ContentReference>
    {
        private readonly IUrlHelper _urlHelper;

        public ContentReferencePropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public object Handle(ContentReference contentReference, PropertyInfo propertyInfo, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            if (contentReference == null || contentReference == ContentReference.EmptyReference)
            {
                return null;
            }

            var url = new Uri(this._urlHelper.ContentUrl(contentReference, contentSerializerSettings.UrlSettings));

            if (contentSerializerSettings.UrlSettings.UseAbsoluteUrls && url.IsAbsoluteUri)
            {
                return url.AbsoluteUri;
            }

            return url.PathAndQuery;
        }
    }
}
