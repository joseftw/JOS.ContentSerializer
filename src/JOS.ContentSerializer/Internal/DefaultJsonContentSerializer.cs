using System;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultJsonContentSerializer : IContentSerializer
    {
        private readonly PropertyManager _propertyManager;

        public DefaultJsonContentSerializer(PropertyManager propertyManager)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
        }

        public string Serialize(IContentData contentData)
        {
            return Execute(contentData, null);
        }

        public string Serialize(IContentData contentData, ContentSerializerSettings settings)
        {
            return Execute(contentData, settings);
        }

        private string Execute(IContentData contentData, ContentSerializerSettings settings)
        {
            var hej = this._propertyManager.GetStructuredData(contentData, settings);
            return "HEJ";
        }
    }
}
