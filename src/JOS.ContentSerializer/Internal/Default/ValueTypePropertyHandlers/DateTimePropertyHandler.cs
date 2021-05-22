using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class DateTimePropertyHandler : IPropertyHandler<DateTime>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public DateTimePropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(DateTime value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(DateTime value, PropertyInfo property, IContentData contentData, IContentSerializerSettings settings)
        {
            return value;
        }
    }
}
