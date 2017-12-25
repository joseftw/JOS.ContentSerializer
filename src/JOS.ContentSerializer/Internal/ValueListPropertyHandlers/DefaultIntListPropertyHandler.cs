using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.ValueListPropertyHandlers
{
    public class DefaultIntListPropertyHandler : IPropertyHandler<IEnumerable<int>>
    {
        public object Handle(IEnumerable<int> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
