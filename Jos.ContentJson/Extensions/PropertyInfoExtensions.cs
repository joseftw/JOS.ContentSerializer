using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using EPiServer.Shell.ObjectEditing;
using Jos.ContentJson.Helpers;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class PropertyInfoExtensions
    {
        private static readonly IPropertyHelperBase PropertyHelperBase = DependencyResolver.Current.GetService<IPropertyHelperBase>() ?? new PropertyHelperBase();

        public static string GetJsonKey(this PropertyInfo propertyInfo)
        {
            var jsonAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(JsonPropertyAttribute));

            return jsonAttribute == null ? propertyInfo.Name.LowerCaseFirstLetter() : jsonAttribute.PropertyName.LowerCaseFirstLetter();
        }

        public static SelectOneAttribute GetSelectOneAttribute(this PropertyInfo propertyInfo)
        {
            var attribute = (SelectOneAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(SelectOneAttribute));
            return attribute;
        }

        public static SelectManyAttribute GetSelectManyAttribute(this PropertyInfo propertyInfo)
        {
            var selectMany = (SelectManyAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(SelectManyAttribute));
            return selectMany;
        }

        public static bool HasSelectAttribute(this PropertyInfo property)
        {
            var selectOne = property.GetSelectOneAttribute();
            if (selectOne != null) return true;

            var selectMany = property.GetSelectManyAttribute();
            return selectMany != null;
        }

        public static object GetProcessedData(this PropertyInfo property, object item)
        {
            var propertyValue = property.GetValue(item, null);

            if (propertyValue == null) return null;

            var propertyHelper = PropertyHelperBase.GetPropertyHelper(propertyValue);

            if (propertyHelper == null) return null;

            var castedProperty = PropertyHelperBase.GetCastedProperty(propertyHelper, propertyValue);

            if (castedProperty == null) return null;

            var parameters = new StructuredDataDto { Property = castedProperty, PropertyInfo = property };
            var structuredData = PropertyHelperBase.GetStructuredData(propertyHelper, parameters);

            return structuredData;
        }

        public static bool PropertyShouldBeIncluded(this PropertyInfo property)
        {
            var jsonIgnoreAttribute = Attribute.IsDefined(property, typeof(JsonIgnoreAttribute));

            if (jsonIgnoreAttribute) return false;

            var displayAttribute = Attribute.IsDefined(property, typeof(DisplayAttribute));
            if (displayAttribute) return true;

            var jsonPropertyAttribute = Attribute.IsDefined(property, typeof(JsonPropertyAttribute));
            return jsonPropertyAttribute;
        }
    }
}
