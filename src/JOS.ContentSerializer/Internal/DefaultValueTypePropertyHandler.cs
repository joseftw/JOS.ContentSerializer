using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultValueTypePropertyHandler : IValueTypePropertyHandler
    {
        public object GetValue(IContentData contentData, PropertyInfo property)
        {
            return property.GetValue(contentData);
        }
    }
}
