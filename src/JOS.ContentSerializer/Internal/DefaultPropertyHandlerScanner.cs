using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPropertyHandlerScanner : IPropertyHandlerScanner
    {
        private static IEnumerable<Type> _propertyHandlers;

        public void Scan()
        {
            var propertyHandlerInterface = typeof(IPropertyHandler<>);
            _propertyHandlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .SelectMany(x => x.GetInterfaces())
                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == propertyHandlerInterface)
                .ToList();
        }

        public IEnumerable<Type> GetAllPropertyHandlers()
        {
            if (_propertyHandlers == null)
            {
                throw new Exception("_propertyHandlers is null, make sure that the Scan method has been run.");
            }
            return _propertyHandlers;
        }
    }
}
