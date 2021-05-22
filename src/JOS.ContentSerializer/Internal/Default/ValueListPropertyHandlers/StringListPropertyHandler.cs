using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class StringListPropertyHandler : IPropertyHandler<IEnumerable<string>>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public StringListPropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(IEnumerable<string> value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(
            IEnumerable<string> value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            return value;
        }
    }
}
