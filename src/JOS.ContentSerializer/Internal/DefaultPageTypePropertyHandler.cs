using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPageTypePropertyHandler : IPropertyHandler<PageType>
    {
        public object Handle(PageType value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return value.Name;
        }
    }
}
