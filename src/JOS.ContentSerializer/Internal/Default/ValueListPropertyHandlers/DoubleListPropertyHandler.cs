using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class DoubleListPropertyHandler : IPropertyHandler<IEnumerable<double>>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public DoubleListPropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(IEnumerable<double> value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(IEnumerable<double> value, PropertyInfo property, IContentData contentData, IContentSerializerSettings settings)
        {
            return value;
        }
    }
}
