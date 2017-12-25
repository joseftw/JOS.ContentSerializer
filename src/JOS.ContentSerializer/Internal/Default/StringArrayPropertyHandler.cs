using System.Linq;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class StringArrayPropertyHandler : IPropertyHandler<string[]>
    {
        public object Handle(string[] value, PropertyInfo property, IContentData contentData)
        {
            if (value == null)
            {
                return (string[])Enumerable.Empty<string>();
            }

            return value;
        }
    }
}
