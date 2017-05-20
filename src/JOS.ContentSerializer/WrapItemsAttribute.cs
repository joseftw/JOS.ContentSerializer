using System;

namespace JOS.ContentSerializer
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
