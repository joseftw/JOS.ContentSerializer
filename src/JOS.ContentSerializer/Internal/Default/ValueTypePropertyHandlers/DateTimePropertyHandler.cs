using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class DateTimePropertyHandler : IPropertyHandler<DateTime>, IPropertyHandler2<DateTime>
    {
        public object Handle(DateTime value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(DateTime value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
