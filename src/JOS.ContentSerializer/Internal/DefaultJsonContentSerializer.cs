using System;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultJsonContentSerializer : IContentSerializer
    {
        private readonly PropertyManager _propertyManager;
        private readonly IPropertyResolver _propertyResolver;

        public DefaultJsonContentSerializer(
            IPropertyResolver propertyResolver,
            PropertyManager propertyManager)
        {
            _propertyResolver = propertyResolver ?? throw new ArgumentNullException(nameof(propertyResolver));
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
            var properties = this._propertyResolver.GetProperties(contentData);
            var hej = this._propertyManager.GetStructuredData(contentData, properties, settings);
            return "HEJ";
        }
    }
}
