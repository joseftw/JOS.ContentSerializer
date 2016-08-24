using EPiServer.Core;

using Jos.ContentJson.Interfaces;
using Jos.ContentJson.Models.Dtos;

namespace Jos.ContentJson.Helpers
{
	public class XhtmlStringPropertyHelper : IPropertyHelper, IXhtmlStringPropertyHelper
	{
		public XhtmlString CastProperty(object propertyValue) {
			return propertyValue as XhtmlString;
		}

		public string GetStructuredData(StructuredDataDto dataDto) {
			var xhtmlString = dataDto.Property as XhtmlString;
			if(xhtmlString == null) {
				return null;
			}
			return xhtmlString.ToHtmlString();
		}
	}
}
