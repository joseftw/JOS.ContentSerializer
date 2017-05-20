using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultLinkItemCollectionPropertyHandler : ILinkItemCollectionPropertyHandler
    {
        private readonly IUrlHelper _urlHelper;

        public DefaultLinkItemCollectionPropertyHandler(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public IEnumerable<object> GetValue(LinkItemCollection linkItemCollection)
        {
            return Execute(linkItemCollection, new UrlSettings());
        }

        public IEnumerable<object> GetValue(LinkItemCollection linkItemCollection, UrlSettings urlSettings)
        {
            return Execute(linkItemCollection, urlSettings);
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
