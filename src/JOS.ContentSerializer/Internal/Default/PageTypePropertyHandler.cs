using System;
using System.Reflection;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal.Default
{
    public class PageTypePropertyHandler : IPropertyHandler<PageType>
    {
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public PageTypePropertyHandler(IContentSerializerSettings contentSerializerSettings)
        {
            _contentSerializerSettings = contentSerializerSettings ?? throw new ArgumentNullException(nameof(contentSerializerSettings));
        }

        public object Handle(
            PageType value,
            PropertyInfo propertyInfo,
            IContentData contentData)
        {
            return Handle(value, propertyInfo, contentData, _contentSerializerSettings);
        }

        public object Handle(
            PageType value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            return value == null ? null : new PageTypeModel(value.Name, value.ID);
        }
    }
}
