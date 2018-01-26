using System;

namespace JOS.ContentSerializer.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ContentSerializerSelectedOptionsOnlyAttribute : Attribute
    {
        public virtual bool SelectedValueOnly { get; set; }
    }
}
