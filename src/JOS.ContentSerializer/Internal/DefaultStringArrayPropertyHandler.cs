using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultStringArrayPropertyHandler : IStringArrayPropertyHandler
    {
        public string[] GetValue(IContentData contentData, PropertyInfo property)
        {
            var value = property.GetValue(contentData);
            if (value == null)
            {
                return new string[0];
            }
            return (string[]) value;
        }
    }
}
