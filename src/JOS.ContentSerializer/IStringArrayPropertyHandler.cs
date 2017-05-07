using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IStringArrayPropertyHandler
    {
        string[] GetValue(IContentData contentData, PropertyInfo property);
    }
}