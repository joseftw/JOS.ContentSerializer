using EPiServer.Core;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IContentReferencePropertyHelper
    {
        ContentReference CastProperty(object propertyValue);
        string GetStructuredData(StructuredDataDto dataDto);
    }
}
