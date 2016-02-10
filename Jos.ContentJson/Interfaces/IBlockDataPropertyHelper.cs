using EPiServer.Core;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IBlockDataPropertyHelper
    {
        ContentData CastProperty(object propertyValue);
        object GetStructuredData(StructuredDataDto dataDto);
    }
}
