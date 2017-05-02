using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IStringPropertyHandler
    {
        object GetValue(IContentData contentData, PropertyInfo propertyInfo);
    }
}
