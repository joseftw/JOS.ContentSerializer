using EPiServer;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IUrlPropertyHelper
    {
        Url CastProperty(object propertyValue);
        string GetStructuredData(StructuredDataDto dataDto);
    }
}
