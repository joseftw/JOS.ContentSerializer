using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPageTypePropertyHandler : PropertyHandler<PageType>
    {
        public override object Handle(object value, PropertyInfo propertyInfo, IContentData contentData)
        {
            var pageType = (PageType)value;
            return pageType.Name;
        }
    }
}
