using EPiServer;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class UrlPropertyHelper : IPropertyHelper, IUrlPropertyHelper
    {
        public Url CastProperty(object propertyValue)
        {
            return propertyValue as Url;
        }

        public string GetStructuredData(StructuredDataDto dataDto)
        {
            var url = dataDto.Property as Url;
            return url.ToPrettyUrl();
        }
    }
}
