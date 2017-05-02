using System;

namespace JOS.ContentSerializer
{
    public class ContentSerializerNameAttribute : Attribute
    {
        public ContentSerializerNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
