using EPiServer.Core;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class PageReferencePropertyHelper : ContentReferencePropertyHelper, IPropertyHelper, IPageReferencePropertyHelper
    {
        public ContentReference CastProperty(object propertyValue)
        {
            return base.CastProperty(propertyValue);
        }

        public string GetStructuredData(StructuredDataDto dataDto)
        {
            return base.GetStructuredData(dataDto);
        }
    }
}
