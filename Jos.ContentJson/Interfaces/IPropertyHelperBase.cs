using System;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
    public interface IPropertyHelperBase
    {
        Type GetPropertyHelper(object propertyValue);
        object GetCastedProperty(Type propertyHelper, object propertyValue);
        object GetStructuredData(Type propertyHelper, StructuredDataDto parameters);
    }
}
