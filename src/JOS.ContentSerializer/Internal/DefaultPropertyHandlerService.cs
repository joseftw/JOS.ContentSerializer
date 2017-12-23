using System;
using System.Linq;
using EPiServer.ServiceLocation;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPropertyHandlerService : IPropertyHandlerService
    {
        public object GetPropertyHandler(Type type)
        {
            var propertyHandlerInterface = typeof(IPropertyHandler<>);
            // TODO CACHE THIS.
            var propertyHandlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .SelectMany(x => x.GetInterfaces())
                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == propertyHandlerInterface)
                .ToList();

            var propertyHandlerType = propertyHandlers.FirstOrDefault(x => x.GetGenericArguments()[0] == type);
            return ServiceLocator.Current.GetInstance(propertyHandlerType);
        }
    }
}
