using System;
using EPiServer.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JOS.ContentSerializer.Internal
{
    public class JsonContentSerializer : IContentJsonSerializer
    {
        private readonly IPropertyManager _propertyManager;
        private readonly IContentSerializerSettings _contentSerializerSettings;
        private static readonly JsonSerializerSettings JsonSerializerSettings;

        static JsonContentSerializer()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public JsonContentSerializer(IPropertyManager propertyManager, IContentSerializerSettings contentSerializerSettings)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public string Serialize(IContentData contentData)
        {
            return Execute(contentData, _contentSerializerSettings);
        }

        public string Serialize(IContentData contentData, IContentSerializerSettings settings)
        {
            return Execute(contentData, settings);
        }

        public object GetStructuredData(IContentData contentData)
        {
            return this._propertyManager.GetStructuredData(contentData, _contentSerializerSettings);
        }

        public object GetStructuredData(IContentData contentData, IContentSerializerSettings settings)
        {
            return this._propertyManager.GetStructuredData(contentData, settings);
        }

        private string Execute(IContentData contentData, IContentSerializerSettings settings)
        {
            var result = this._propertyManager.GetStructuredData(contentData, settings);
            return JsonConvert.SerializeObject(result, JsonSerializerSettings);
        }
    }
}
