using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IValueTypePropertyHandler
    {
        object GetValue(IContentData contentData, PropertyInfo property);
    }
}