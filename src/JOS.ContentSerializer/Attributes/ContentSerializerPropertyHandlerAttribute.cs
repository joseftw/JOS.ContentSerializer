using System;

namespace JOS.ContentSerializer.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ContentSerializerPropertyHandlerAttribute : Attribute
    {
        public ContentSerializerPropertyHandlerAttribute(Type propertyHandler)
        {
            PropertyHandler = propertyHandler;
        }

        public Type PropertyHandler { get; }
    }
}
