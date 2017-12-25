using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.ValueListPropertyHandlers
{
    public class DateTimeListPropertyHandler : IPropertyHandler<IEnumerable<DateTime>>
    {
        public object Handle(IEnumerable<DateTime> value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
