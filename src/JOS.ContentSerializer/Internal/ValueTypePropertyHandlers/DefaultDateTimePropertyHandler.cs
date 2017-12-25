using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.ValueTypePropertyHandlers
{
    public class DefaultDateTimePropertyHandler : IPropertyHandler<DateTime>
    {
        public object Handle(DateTime value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
