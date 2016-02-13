using EPiServer.Core;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class ContentAreaPropertyHelper : IPropertyHelper, IContentAreaPropertyHelper
    {
        public ContentArea CastProperty(object propertyValue)
        {
            return propertyValue as ContentArea;
        }

        public object GetStructuredData(StructuredDataDto dataDto)
        {
            var contentArea = dataDto.Property as ContentArea;
            return contentArea.GetStructuredData();
        }
    }
}
