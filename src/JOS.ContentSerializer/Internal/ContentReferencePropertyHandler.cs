using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class ContentReferencePropertyHandler : IPropertyHandler<ContentReference>
    {
        private readonly IUrlHelper _urlHelper;

        public ContentReferencePropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public object Handle(ContentReference value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, new ContentReferenceSettings()); // TODO Allow injection of settings
        }

        private object Execute(ContentReference contentReference, ContentReferenceSettings contentReferenceSettings)
        {
            var url = new Uri(this._urlHelper.ContentUrl(contentReference, contentReferenceSettings));

            if (contentReferenceSettings.UseAbsoluteUrls && url.IsAbsoluteUri)
            {
                return url.AbsoluteUri;
            }

            return url.PathAndQuery;
        }
    }
}
