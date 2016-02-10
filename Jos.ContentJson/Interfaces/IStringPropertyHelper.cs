using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IStringPropertyHelper
    {
        string CastProperty(object propertyValue);
        object GetStructuredData(StructuredDataDto dataDto);
    }
}
