using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IStringArrayPropertyHelper
    {
        string[] CastProperty(object propertyValue);
        object GetStructuredData(StructuredDataDto dataDto);
    }
}
