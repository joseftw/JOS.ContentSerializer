using System;
using System.Reflection;
using EPiServer.Shell.ObjectEditing;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string GetJsonKey(this PropertyInfo propertyInfo)
        {
            var jsonAttribute = (JsonPropertyAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(JsonPropertyAttribute));

            return jsonAttribute == null ? propertyInfo.Name : jsonAttribute.PropertyName.LowerCaseFirstLetter();
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
    }
}
