using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class BoolPropertyHandler : IPropertyHandler<bool>, IPropertyHandler2<bool>
    {
        public object Handle(bool value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(bool value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
