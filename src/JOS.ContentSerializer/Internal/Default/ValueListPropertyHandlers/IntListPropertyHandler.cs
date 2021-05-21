using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class IntListPropertyHandler : IPropertyHandler<IEnumerable<int>>, IPropertyHandler2<IEnumerable<int>>
    {
        public object Handle(IEnumerable<int> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(IEnumerable<int> value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
