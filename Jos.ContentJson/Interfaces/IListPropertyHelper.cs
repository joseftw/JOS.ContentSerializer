using System.Collections;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IListPropertyHelper
    {
        IList CastProperty(object propertyValue);
        IList GetStructuredData(StructuredDataDto dataDto);
    }
}
