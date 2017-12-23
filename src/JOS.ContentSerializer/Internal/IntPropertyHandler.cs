using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class IntPropertyHandler : IPropertyHandler<int>
    {
        public object Handle(int value, PropertyInfo property, IContentData contentData)
        {
            return property.GetValue(contentData);
        }
    }
}
