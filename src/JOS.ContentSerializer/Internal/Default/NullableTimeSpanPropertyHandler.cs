using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class NullableTimeSpanPropertyHandler : IPropertyHandler<TimeSpan?>, IPropertyHandler2<TimeSpan?>
    {
        public object Handle(TimeSpan? value, PropertyInfo property, IContentData contentData)
        {
            return value?.ToString();
        }

        public object Handle2(TimeSpan? value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
