using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;

namespace JOS.ContentSerializer.Internal.Default
{
    public class LinkItemCollectionPropertyHandler : IPropertyHandler<LinkItemCollection>
    {
        private readonly IUrlHelper _urlHelper;

        public LinkItemCollectionPropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public object Handle(LinkItemCollection value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, new UrlSettings()); // TODO allow injection of UrlSettings
        }

        private IEnumerable<object> Execute(LinkItemCollection linkItemCollection, UrlSettings urlSettings)
        {
            var links = new List<LinkItem>();

            foreach (var link in linkItemCollection)
            {
                string prettyUrl = null;
                if (link.ReferencedPermanentLinkIds.Any())
                {
                    var url = new Url(link.Href);
                    prettyUrl = this._urlHelper.ContentUrl(url, urlSettings);
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
