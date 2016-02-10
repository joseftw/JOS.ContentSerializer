using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class StringArrayPropertyHelper : IPropertyHelper, IStringArrayPropertyHelper
    {
        public string[] CastProperty(object propertyValue)
        {
            return propertyValue as string[];
        }

        public object GetStructuredData(StructuredDataDto dataDto)
        {
            var array = dataDto.Property as string[];
            return array;
        }
    }
}
