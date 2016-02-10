using EPiServer.SpecializedProperties;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface ILinkItemCollectionPropertyHelper
    {
        LinkItemCollection CastProperty(object propertyValue);
        object GetStructuredData(StructuredDataDto dataDto);
    }
}
