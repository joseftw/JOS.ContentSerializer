using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IStringArrayPropertyHandler
    {
        object GetValue(IContentData contentData, PropertyInfo property);
    }
}