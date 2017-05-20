using System;

namespace JOS.ContentSerializer.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class WrapItemsAttribute : Attribute
    {
        public WrapItemsAttribute(bool wrapItems)
        {
            WrapItems = wrapItems;
        }

        public bool WrapItems { get; }
    }
}
