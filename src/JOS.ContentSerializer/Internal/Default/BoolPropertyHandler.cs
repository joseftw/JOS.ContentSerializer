using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class BoolPropertyHandler : IPropertyHandler<bool>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public BoolPropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(bool value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(
            bool value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            return value;
        }
    }
}
