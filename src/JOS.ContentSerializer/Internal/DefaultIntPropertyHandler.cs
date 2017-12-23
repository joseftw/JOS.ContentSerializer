using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultIntPropertyHandler : IPropertyHandler<int>
    {
        public object Handle(int value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
