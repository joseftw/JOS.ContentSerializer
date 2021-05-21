using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class IntPropertyHandler : IPropertyHandler<int>, IPropertyHandler2<int>
    {
        public object Handle(int value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(int value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
