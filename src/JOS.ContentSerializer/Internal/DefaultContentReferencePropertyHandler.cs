using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentReferencePropertyHandler : PropertyHandler<ContentReference>
    {
        private readonly IUrlHelper _urlHelper;

        public DefaultContentReferencePropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public override object Handle(object value, PropertyInfo propertyInfo, IContentData contentData)
        {
            var contentReference = (ContentReference)value;
            return Execute(contentReference, new ContentReferenceSettings()); // TODO Allow injection of settings
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
