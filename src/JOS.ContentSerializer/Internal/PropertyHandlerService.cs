using System;
using System.Linq;
using EPiServer.ServiceLocation;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyHandlerService : IPropertyHandlerService
    {
        private readonly IPropertyHandlerScanner _propertyHandlerScanner;

        public PropertyHandlerService(IPropertyHandlerScanner propertyHandlerScanner)
        {
            _propertyHandlerScanner = propertyHandlerScanner ?? throw new ArgumentNullException(nameof(propertyHandlerScanner));
        }

        public object GetPropertyHandler(Type type)
        {
            var propertyHandlers = this._propertyHandlerScanner.GetAllPropertyHandlers();
            var propertyHandlerType = propertyHandlers.FirstOrDefault(x => x.GetGenericArguments()[0] == type);
            return propertyHandlerType == null
                ? null
                : ServiceLocator.Current.GetInstance(propertyHandlerType);
        }
    }
}
