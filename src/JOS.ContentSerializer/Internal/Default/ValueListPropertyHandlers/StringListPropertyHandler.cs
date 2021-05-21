using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class StringListPropertyHandler : IPropertyHandler<IEnumerable<string>>, IPropertyHandler2<IEnumerable<string>>
    {
        public object Handle(IEnumerable<string> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(IEnumerable<string> value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
