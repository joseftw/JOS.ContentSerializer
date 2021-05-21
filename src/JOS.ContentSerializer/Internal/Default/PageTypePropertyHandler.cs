using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal.Default
{
    public class PageTypePropertyHandler : IPropertyHandler<PageType>, IPropertyHandler2<PageType>
    {
        public object Handle(PageType value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return value == null ? null : new PageTypeModel(value.Name, value.ID);
        }

        public object Handle2(PageType value, PropertyInfo propertyInfo, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, propertyInfo, contentData);
        }
    }
}
