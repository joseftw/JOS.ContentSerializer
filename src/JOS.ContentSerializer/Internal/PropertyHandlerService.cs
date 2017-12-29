using System;
using System.Linq;
using System.Reflection;
using EPiServer.ServiceLocation;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyHandlerService : IPropertyHandlerService
    {
        private readonly Type _propertyHandlerType = typeof(IPropertyHandler<>);

        public object GetPropertyHandler(PropertyInfo property)
        {
            var customPropertyHandlerAttribute = property.GetCustomAttribute<ContentSerializerPropertyHandlerAttribute>();
            if (customPropertyHandlerAttribute != null)
            {
                return ServiceLocator.Current.GetInstance(customPropertyHandlerAttribute.PropertyHandler);
            }

            var propertyHandlerType = this._propertyHandlerType.MakeGenericType(property.PropertyType);

            return ServiceLocator.Current.GetInstance(propertyHandlerType);
        }
    }
}
