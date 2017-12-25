using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.ValueListPropertyHandlers
{
    public class DefaultDoubleListPropertyHandler : IPropertyHandler<IEnumerable<double>>
    {
        public object Handle(IEnumerable<double> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
