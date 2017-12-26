using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class ContentReferencePropertyHandler : IPropertyHandler<ContentReference>
    {
        private readonly IUrlHelper _urlHelper;
        private readonly ContentSerializerSettings _contentSerializerSettings;

        public ContentReferencePropertyHandler(IUrlHelper urlHelper) : this(urlHelper, null)
        {
        }

        public ContentReferencePropertyHandler(IUrlHelper urlHelper, ContentSerializerSettings contentSerializerSettings)
        {
            _urlHelper = urlHelper;
            _contentSerializerSettings = contentSerializerSettings;
        }

        public object Handle(ContentReference value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, this._contentSerializerSettings.UrlSettings);
        }

        private object Execute(ContentReference contentReference, IUrlSettings urlSettings)
        {
            var url = new Uri(this._urlHelper.ContentUrl(contentReference, urlSettings));

            if (urlSettings.UseAbsoluteUrls && url.IsAbsoluteUri)
            {
                return url.AbsoluteUri;
            }

            return url.PathAndQuery;
        }
    }
}
