using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class DoublePropertyHandler : IPropertyHandler<double>
    {
        public object Handle(double value, PropertyInfo property, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            return value;
        }
    }
}
