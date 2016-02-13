using System;
using System.Collections;
using EPiServer.Core;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class PropertyHelperBase : IPropertyHelperBase
    {
        public Type GetPropertyHelper(object propertyValue)
        {
            var propertyType = propertyValue.GetType().Name;
    
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

            var helperName = string.Format("{0}.{1}PropertyHelper", Constants.PropertyHelpersNameSpace, propertyType);
            var helperType = Type.GetType(helperName);

            return helperType;
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
    }
}
