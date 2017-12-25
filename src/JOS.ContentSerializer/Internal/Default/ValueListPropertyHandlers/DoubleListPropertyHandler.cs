using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class DoubleListPropertyHandler : IPropertyHandler<IEnumerable<double>>
    {
        public object Handle(IEnumerable<double> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
