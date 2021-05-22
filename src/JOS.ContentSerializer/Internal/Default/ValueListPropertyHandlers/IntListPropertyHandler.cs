using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class IntListPropertyHandler : IPropertyHandler<IEnumerable<int>>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public IntListPropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(IEnumerable<int> value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(IEnumerable<int> value, PropertyInfo property, IContentData contentData, IContentSerializerSettings settings)
        {
            return value;
        }
    }
}
