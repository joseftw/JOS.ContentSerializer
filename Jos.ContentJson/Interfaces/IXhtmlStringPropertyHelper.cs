using EPiServer.Core;

using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Interfaces
{
	public interface IXhtmlStringPropertyHelper
	{
		XhtmlString CastProperty(object propertyValue);
		string GetStructuredData(StructuredDataDto dataDto);
	}
}
