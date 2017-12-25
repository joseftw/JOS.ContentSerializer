using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.ValueListPropertyHandlers
{
    public class StringListPropertyHandler : IPropertyHandler<IEnumerable<string>>
    {
        public object Handle(IEnumerable<string> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
