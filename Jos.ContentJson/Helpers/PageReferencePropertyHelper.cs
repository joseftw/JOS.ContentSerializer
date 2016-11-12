using EPiServer.Core;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class PageReferencePropertyHelper : ContentReferencePropertyHelper, IPageReferencePropertyHelper
    {
        public new ContentReference CastProperty(object propertyValue)
        {
            return base.CastProperty(propertyValue);
        }

        public new string GetStructuredData(StructuredDataDto dataDto)
        {
            return base.GetStructuredData(dataDto);
        }
    }
}
