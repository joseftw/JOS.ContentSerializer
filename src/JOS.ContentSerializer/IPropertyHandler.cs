using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IPropertyHandler<in T>
    {
        object Handle(T value, PropertyInfo property, IContentData contentData);
    }
}
