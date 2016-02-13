using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IValueTypePropertyHelper
    {
        object CastProperty(object propertyValue);
        object GetStructuredData(StructuredDataDto dataDto);
    }
}
