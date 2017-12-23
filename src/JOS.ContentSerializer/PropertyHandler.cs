using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public abstract class PropertyHandler<T>
    {
        public abstract object Handle(object value, PropertyInfo property, IContentData contentData);
    }
}
