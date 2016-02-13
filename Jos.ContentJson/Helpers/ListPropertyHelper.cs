using System.Collections;
using Jos.ContentJson.Extensions;
using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
    public class ListPropertyHelper : IPropertyHelper, IListPropertyHelper
    {
        public IList CastProperty(object propertyValue)
        {
            return propertyValue as IList;
        }

        public IList GetStructuredData(StructuredDataDto dataDto)
        {
            var list = dataDto.Property as IList;
            return list.GetStructuredData();
        }
    }
}
