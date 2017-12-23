using System;

namespace JOS.ContentSerializer
{
    public interface IPropertyHandlerService
    {
        object GetPropertyHandler(Type type);
    }
}
