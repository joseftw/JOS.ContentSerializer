using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.Core;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class PropertyHelperBase : IPropertyHelperBase
    {
        private static readonly List<Type> RegisteredCustomPropertyHelpers = GetCustomRegisteredPropertyHelpers();
        private static readonly List<Type> DefaultPropertyHelpers = GetDefaultPropertyHelpers(); 

        public Type GetPropertyHelper(object propertyValue)
        {
            var propertyType = propertyValue.GetType().Name;

            //Check if we have any helper registered, first look in the CustomPropertyHelpers, if no registered, look in the DefaultOnes.
            var helperName = $"{propertyType}PropertyHelper";
            var helper = RegisteredCustomPropertyHelpers.FirstOrDefault(x => x.Name == helperName) ?? DefaultPropertyHelpers.FirstOrDefault(x => x.Name == helperName);

            if (helper != null) return helper;

            //Handle specialcases like string[], BlockData, PropertyList etc. Reason is that you cant name a helper String[]PropertyHelper,
            //so we are "cleaning" the name making it StringArrayPropertyHelper.
            if (propertyValue is string[])
            {
                propertyType = Constants.StringArrayTypeName;
            }
            else if (propertyValue is BlockData)
            {
                propertyType = Constants.BlockTypeName;
            }
            else if (propertyValue is IList)
            {
                propertyType = Constants.ListTypeName;
            }
            else if (propertyValue is ValueType)
            {
                propertyType = Constants.ValueTypeName;
            }
            
            //Look again if we can find the helper, now with a clean name.
            helperName = $"{propertyType}PropertyHelper";
            helper = RegisteredCustomPropertyHelpers.FirstOrDefault(x => x.Name == helperName) ?? DefaultPropertyHelpers.FirstOrDefault(x => x.Name == helperName);

            return helper;
        }

        public object GetCastedProperty(Type propertyHelper, object propertyValue)
        {
            var helper = Activator.CreateInstance(propertyHelper);
            var propertyCastMethod = propertyHelper.GetMethod(Constants.CastMethodName);
            var castedProperty = propertyCastMethod.Invoke(helper, new[] { propertyValue });

            return castedProperty;

        }

        public object GetStructuredData(Type propertyHelper, StructuredDataDto parameters)
        {
            var helper = Activator.CreateInstance(propertyHelper);
            var propertyStructuredDataMethod = propertyHelper.GetMethod(Constants.GetStructuredDataMethodName);
            var structuredData = propertyStructuredDataMethod.Invoke(helper, new object[] { parameters });

            return structuredData;
        }

        private static List<Type> GetCustomRegisteredPropertyHelpers()
        {
            var type = typeof(IPropertyHelper);
            var josAssembly = Assembly.GetExecutingAssembly();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != josAssembly.FullName);
            
            var registeredHelpers = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .ToList();

            return registeredHelpers;
        }

        private static List<Type> GetDefaultPropertyHelpers()
        {
            var type = typeof (IPropertyHelper);
            var josAssembly = Assembly.GetExecutingAssembly();
            var defaultHelpers = josAssembly.GetTypes().Where(p => type.IsAssignableFrom(p)).ToList();
            return defaultHelpers;
        } 
    }
}
