using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;

namespace JOS.ContentSerializer.Internal.Default
{
    public class LinkItemCollectionPropertyHandler : IPropertyHandler<LinkItemCollection>, IPropertyHandler2<LinkItemCollection>
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public LinkItemCollectionPropertyHandler(IUrlHelper urlHelper, IContentSerializerSettings contentSerializerSettings)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(LinkItemCollection linkItemCollection, PropertyInfo propertyInfo, IContentData contentData)
        {
            return HandleInternal(linkItemCollection, this._contentSerializerSettings);
        }

        public object Handle2(LinkItemCollection linkItemCollection, PropertyInfo propertyInfo, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return HandleInternal(linkItemCollection, contentSerializerSettings);
        }

        private object HandleInternal(LinkItemCollection linkItemCollection, IContentSerializerSettings contentSerializerSettings)
        {
            if (linkItemCollection == null)
            {
                return null;
            }
            var links = new List<LinkItem>();

            foreach (var link in linkItemCollection)
            {
                string prettyUrl = null;
                if (link.ReferencedPermanentLinkIds.Any())
                {
                    var url = new Url(link.Href);
                    prettyUrl = this._urlHelper.ContentUrl(url, contentSerializerSettings.UrlSettings);
                }
                links.Add(new LinkItem
                {
                    Text = link.Text,
                    Target = link.Target,
                    Title = link.Title,
                    Href = prettyUrl ?? link.Href
                });
            }

            return links;
        }
    }
}
