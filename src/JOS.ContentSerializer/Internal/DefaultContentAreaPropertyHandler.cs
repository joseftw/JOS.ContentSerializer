using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentAreaPropertyHandler : IContentAreaPropertyHandler
    {
        private readonly IContentLoader _contentLoader;

        public DefaultContentAreaPropertyHandler(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
        }

        public IEnumerable<IContentData> GetValue(ContentArea contentArea)
        {
            return GetValue(contentArea, DefaultContentSerializerSettings.Instance);
        }

        public IEnumerable<IContentData> GetValue(ContentArea contentArea, ContentSerializerSettings settings)
        {
            if (contentArea?.Items == null || !contentArea.Items.Any())
            {
                return Enumerable.Empty<IContentData>();
            }

            var content = new List<IContentData>();
            foreach (var contentAreaItem in contentArea.Items)
            {
                var loadedContent = this._contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
                if (loadedContent != null)
                {
                    content.Add(loadedContent);
                }
            }

            return content;
        }
    }
}
