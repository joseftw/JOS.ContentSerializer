using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers
{
    public class DateTimeListPropertyHandler : IPropertyHandler<IEnumerable<DateTime>>, IPropertyHandler2<IEnumerable<DateTime>>
    {
        public object Handle(IEnumerable<DateTime> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }

        public object Handle2(IEnumerable<DateTime> value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            return Handle(value, property, contentData);
        }
    }
}
