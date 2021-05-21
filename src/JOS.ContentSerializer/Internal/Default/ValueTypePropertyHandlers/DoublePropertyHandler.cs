using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class DoublePropertyHandler : IPropertyHandler<double>, IPropertyHandler2<double>
    {
        public object Handle(double value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(double value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
