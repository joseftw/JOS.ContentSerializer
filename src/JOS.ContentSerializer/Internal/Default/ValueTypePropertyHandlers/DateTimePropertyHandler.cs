using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers
{
    public class DateTimePropertyHandler : IPropertyHandler<DateTime>
    {
        public object Handle(DateTime value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
