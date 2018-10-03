using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal.Default
{
    public class PageTypePropertyHandler : IPropertyHandler<PageType>
    {
        public object Handle(PageType value, PropertyInfo propertyInfo, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            return value == null ? null : new PageTypeModel(value.Name, value.ID);
        }
    }
}
