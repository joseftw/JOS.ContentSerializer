using EPiServer.Core;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class BlockDataPropertyHelper : IPropertyHelper, IBlockDataPropertyHelper
    {
        public ContentData CastProperty(object propertyValue)
        {
            return propertyValue as ContentData;
        }

        public object GetStructuredData(StructuredDataDto dataDto)
        {
            var contentData = dataDto.Property as ContentData;
            return contentData.GetStructuredDictionary();
        }
    }
}
