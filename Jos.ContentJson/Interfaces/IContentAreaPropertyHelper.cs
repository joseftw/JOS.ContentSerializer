using EPiServer.Core;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IContentAreaPropertyHelper
    {
        ContentArea CastProperty(object propertyValue);
        object GetStructuredData(StructuredDataDto dataDto);
    }
}
