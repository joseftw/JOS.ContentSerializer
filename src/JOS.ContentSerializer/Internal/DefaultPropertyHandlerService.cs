using System;
using System.Linq;
using EPiServer.ServiceLocation;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPropertyHandlerService : IPropertyHandlerService
    {
        private readonly IPropertyHandlerScanner _propertyHandlerScanner;

        public DefaultPropertyHandlerService(IPropertyHandlerScanner propertyHandlerScanner)
        {
            _propertyHandlerScanner = propertyHandlerScanner ?? throw new ArgumentNullException(nameof(propertyHandlerScanner));
        }

        public object GetPropertyHandler(Type type)
        {
            var propertyHandlers = this._propertyHandlerScanner.GetAllPropertyHandlers();
            var propertyHandlerType = propertyHandlers.FirstOrDefault(x => x.GetGenericArguments()[0] == type);
            return ServiceLocator.Current.GetInstance(propertyHandlerType);
        }
    }
}
