using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class NullableTimeSpanPropertyHandler : IPropertyHandler<TimeSpan?>
    {
        public object Handle(TimeSpan? value, PropertyInfo property, IContentData contentData)
        {
            return value?.ToString();
        }
    }
}
