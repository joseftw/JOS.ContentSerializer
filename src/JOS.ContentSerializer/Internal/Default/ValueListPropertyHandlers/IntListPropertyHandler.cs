using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class IntListPropertyHandler : IPropertyHandler<IEnumerable<int>>
    {
        public object Handle(IEnumerable<int> value, PropertyInfo property, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            return value;
        }
    }
}
