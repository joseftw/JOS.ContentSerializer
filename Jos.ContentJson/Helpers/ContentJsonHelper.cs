using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class ContentJsonHelper : ContentJsonHelperBase, IContentJsonHelper
    {
        /// <summary>
        /// Looks for a 
        /// </summary>
        /// <param name="jsonProperties"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Dictionary<string, object> CreatePropertyDictionary(IEnumerable<PropertyInfo> jsonProperties, ContentData content)
        {
            var propertyDict = new Dictionary<string, object>();

            foreach (var property in jsonProperties)
            {
                var propertyValue = property.GetValue(content, null);

                if (propertyValue == null)
                {
                    continue;
                }
                    
                var jsonKey = property.GetJsonKey();
                var propertyHelperNamespace = GetPropertyHelperNamespace(propertyValue);

                if (propertyHelperNamespace != null)
                {
                    var helper = Activator.CreateInstance(propertyHelperNamespace);
                    var propertyCastMethod = helper.GetType().GetMethod(Constants.CastMethodName);
                    var propertyStructuredDataMethod = helper.GetType().GetMethod(Constants.GetStructuredDataMethodName);
                    var castedProperty = propertyCastMethod.Invoke(helper, new[] {propertyValue});

                    if(castedProperty == null) continue;

                    var parameters = new StructuredDataDto {Property = castedProperty, PropertyInfo = property};
                    var structuredData = propertyStructuredDataMethod.Invoke(helper, new object[] {parameters});
                    propertyDict.Add(jsonKey, structuredData);
                }
            }

            return propertyDict;
        }
        
        public object GetLoadedContentAreaItem(ContentAreaItem contentAreaItem)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var loadedItem = contentLoader.Get<ContentData>(contentAreaItem.ContentLink);
            var itemAsDictionary = loadedItem.GetStructuredDictionary();
            return itemAsDictionary;
        } 

        private Type GetPropertyHelperNamespace(object propertyValue)
        {
            var helperNameFormat = "{0}.{1}PropertyHelper";
            var propertyType = propertyValue.GetType().Name;
            if (propertyValue is string[])
            {
                propertyType = Constants.StringArrayTypeName;
            }
            else if (propertyValue is BlockData)
            {
                propertyType = Constants.BlockTypeName;
            }

            var helperName = string.Format(helperNameFormat, Constants.PropertyHelpersNameSpace, propertyType);
            var helperType = Type.GetType(helperName);

            return helperType;
        }
    }
}