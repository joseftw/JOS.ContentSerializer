using System.Reflection;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPropertyNameStrategy : IPropertyNameStrategy
    {
        public string GetPropertyName(PropertyInfo property)
        {
            var nameAttribute = property.GetCustomAttribute<ContentSerializerNameAttribute>();
            return nameAttribute == null ? property.Name : nameAttribute.Name;
        }
    }
}
