using System.Reflection;

namespace JOS.ContentSerializer
{
    public interface IPropertyNameStrategy
    {
        string GetPropertyName(PropertyInfo property);
    }
}
