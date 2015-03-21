using System;
namespace ContentJson.Extensions
{
    public static class StringExtensions
    {
        public static string LowerCaseFirstLetter(this string propertyName)
        {
            //This will like...never happen.
            if (string.IsNullOrWhiteSpace(propertyName)) return "missingPropertyName";

            if (propertyName.Length == 1) return propertyName.ToLower();

            return Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}
