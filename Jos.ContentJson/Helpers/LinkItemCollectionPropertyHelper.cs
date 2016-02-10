using EPiServer.SpecializedProperties;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class LinkItemCollectionPropertyHelper : IPropertyHelper, ILinkItemCollectionPropertyHelper
    {
        public LinkItemCollection CastProperty(object propertyValue)
        {
            return propertyValue as LinkItemCollection;
        }

        public object GetStructuredData(StructuredDataDto dataDto)
        {
            var linkItemCollection = dataDto.Property as LinkItemCollection;
            return linkItemCollection.GetStructuredData();
        }
    }
}
