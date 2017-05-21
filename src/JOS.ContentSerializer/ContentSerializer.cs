using System;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public class ContentSerializer
    {
        private readonly IContentSerializer _contentSerializer;

        public ContentSerializer(IContentSerializer contentSerializer)
        {
            _contentSerializer = contentSerializer ?? throw new ArgumentNullException(nameof(contentSerializer));
        }

        public string ToJson(IContentData contentData)
        {
            return this._contentSerializer.Serialize(contentData);
        }

        public string ToJson(IContentData contentData, ContentSerializerSettings settings)
        {
            return this._contentSerializer.Serialize(contentData, settings);
        }
    }
}
