using System.Reflection;

namespace JOS.ContentSerializer
{
    public interface IPropertyHandlerService
    {
        object GetPropertyHandler(PropertyInfo property);
    }
}
