using System.Reflection;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyNameStrategy : IPropertyNameStrategy
    {
        public string GetPropertyName(PropertyInfo property)
        {
            var nameAttribute = property.GetCustomAttribute<ContentSerializerNameAttribute>();
            return nameAttribute == null ? property.Name : nameAttribute.Name;
        }
    }
}
