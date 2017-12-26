using System;
using System.Linq;
using System.Reflection;
using EPiServer.ServiceLocation;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyHandlerService : IPropertyHandlerService
    {
        private readonly IPropertyHandlerScanner _propertyHandlerScanner;

        public PropertyHandlerService(IPropertyHandlerScanner propertyHandlerScanner)
        {
            _propertyHandlerScanner = propertyHandlerScanner ?? throw new ArgumentNullException(nameof(propertyHandlerScanner));
        }

        public object GetPropertyHandler(PropertyInfo property)
        {
            var customPropertyHandlerAttribute = property.GetCustomAttribute<ContentSerializerPropertyHandlerAttribute>();
            if (customPropertyHandlerAttribute != null)
            {
                return ServiceLocator.Current.GetInstance(customPropertyHandlerAttribute.PropertyHandler);
            }
            var propertyHandlers = this._propertyHandlerScanner.GetAllPropertyHandlers();
            var propertyHandlerType = propertyHandlers.FirstOrDefault(x => x.GetGenericArguments()[0] == property.PropertyType);
            return propertyHandlerType == null
                ? null
                : ServiceLocator.Current.GetInstance(propertyHandlerType);
        }
    }
}
