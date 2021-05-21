using System;
using System.Reflection;
using EPiServer.ServiceLocation;
using JOS.ContentSerializer.Attributes;

namespace JOS.ContentSerializer.Internal
{
    public class PropertyHandlerService : IPropertyHandlerService
    {
        private readonly Type _propertyHandler2Type = typeof(IPropertyHandler2<>);

        private readonly Type _propertyHandlerType = typeof(IPropertyHandler<>);

        public object GetPropertyHandler(PropertyInfo property)
        {
            if (property == null)
            {
                return null;
            }
            var customPropertyHandlerAttribute = property.GetCustomAttribute<ContentSerializerPropertyHandlerAttribute>();
            if (customPropertyHandlerAttribute != null)
            {
                ServiceLocator.Current.TryGetExistingInstance(customPropertyHandlerAttribute.PropertyHandler, out var attributePropertyHandler);
                return attributePropertyHandler;
            }

            var propertyHandlerType = this._propertyHandlerType.MakeGenericType(property.PropertyType);
            if (ServiceLocator.Current.TryGetExistingInstance(propertyHandlerType, out var propertyHandler) &&
                propertyHandler.GetType().Assembly != typeof(PropertyHandlerService).Assembly)
            {
                // Non-JOS implementations of the IPropertyHandler are considered first in order to maintain legacy functionality.
                return propertyHandler;
            }

            var propertyHandler2Type = this._propertyHandler2Type.MakeGenericType(property.PropertyType);
            if (propertyHandler2Type.IsAssignableFrom(propertyHandler?.GetType()))
            {
                // All internal JOS handlers implement both IPropertyHandler and IPropertyHandler2, so no need to retrieve
                // the handler through the second interface.
                return propertyHandler;
            }
            if (ServiceLocator.Current.TryGetExistingInstance(propertyHandler2Type, out var propertyHandler2))
            {
                // Non-JOS implementations of the IPropertyHandler2 interface.
                return propertyHandler2;
            }

            return null;
        }
    }
}
