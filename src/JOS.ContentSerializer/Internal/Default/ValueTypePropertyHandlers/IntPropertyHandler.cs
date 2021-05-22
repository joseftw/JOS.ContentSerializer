using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class IntPropertyHandler : IPropertyHandler<int>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public IntPropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(int value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(int value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            return value;
        }
    }
}
