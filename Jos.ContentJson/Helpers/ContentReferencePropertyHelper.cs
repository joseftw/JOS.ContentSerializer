using EPiServer.Core;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class ContentReferencePropertyHelper : IPropertyHelper, IContentReferencePropertyHelper
    {
        public ContentReference CastProperty(object propertyValue)
        {
            return propertyValue as ContentReference;
        }

        public string GetStructuredData(StructuredDataDto dataDto)
        {
            var contentReference = dataDto.Property as ContentReference;
            return contentReference.GetUrl();
        }
    }
}
