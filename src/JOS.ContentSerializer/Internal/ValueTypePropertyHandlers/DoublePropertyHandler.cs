using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.ValueTypePropertyHandlers
{
    public class DoublePropertyHandler : IPropertyHandler<double>
    {
        public object Handle(double value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
