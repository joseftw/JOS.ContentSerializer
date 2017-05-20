using System;
using EPiServer.Core;
using Newtonsoft.Json;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultJsonContentSerializer : IContentSerializer
    {
        private readonly IPropertyManager _propertyManager;

        public DefaultJsonContentSerializer(IPropertyManager propertyManager)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
        }

        public string Serialize(IContentData contentData)
        {
            return Execute(contentData, new ContentSerializerSettings());
        }

        public string Serialize(IContentData contentData, ContentSerializerSettings settings)
        {
            return Execute(contentData, settings);
        }

        private string Execute(IContentData contentData, ContentSerializerSettings settings)
        {
            var result = this._propertyManager.GetStructuredData(contentData, settings);
            return JsonConvert.SerializeObject(result);
        }
    }
}
