using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class IntPropertyHandler : PropertyHandler<int>
    {
        public override object Handle(object value, PropertyInfo property, IContentData contentData)
        {
            return property.GetValue(contentData);
        }
    }
}
