using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class BoolPropertyHandler : IPropertyHandler<bool>
    {
        public object Handle(bool value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
