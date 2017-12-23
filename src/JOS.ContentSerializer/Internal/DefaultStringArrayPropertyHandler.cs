using System.Linq;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultStringArrayPropertyHandler : IPropertyHandler<string[]>
    {
        public object Handle(string[] value, PropertyInfo property, IContentData contentData)
        {
            var propertyValue = property.GetValue(contentData);
            if (propertyValue == null)
            {
                return (string[])Enumerable.Empty<string>();
            }
            return (string[])propertyValue;
        }
    }
}
