using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class ValueTypePropertyHelper : IPropertyHelper, IValueTypePropertyHelper
    {
        public object CastProperty(object propertyValue)
        {
            return propertyValue;
        }

        public object GetStructuredData(StructuredDataDto dataDto)
        {
            return dataDto.Property;
        }
    }
}
