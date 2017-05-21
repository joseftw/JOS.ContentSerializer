using System.Linq;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultStringArrayPropertyHandler : IStringArrayPropertyHandler
    {
        public object GetValue(IContentData contentData, PropertyInfo property)
        {
            var value = property.GetValue(contentData);
            if (value == null)
            {
                return (string[])Enumerable.Empty<string>();
            }
            return (string[]) value;
        }
    }
}
