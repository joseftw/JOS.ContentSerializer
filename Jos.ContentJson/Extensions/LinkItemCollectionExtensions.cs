using System.Collections.Generic;
using EPiServer.SpecializedProperties;
using Jos.ContentJson.Models.LinkItemCollection;
using Newtonsoft.Json;

namespace Jos.ContentJson.Extensions
{
    public static class LinkItemCollectionExtensions
    {
        public static string ToJson(this LinkItemCollection linkItemCollection)
        {
            var structuredData = GetStructuredData(linkItemCollection);
            var json = JsonConvert.SerializeObject(structuredData);
            return json;
        }

        public static IEnumerable<LinkItemDto> GetStructuredData(this LinkItemCollection linkItemCollection)
        {
            var links = new List<LinkItemDto>();
            foreach (var link in linkItemCollection)
            {

                var linkItemDto = new LinkItemDto
                {
                    Href = link.Href.StartsWith("mailto:") ? link.Href : link.UrlResolver.Service.GetUrl(link.Href),
                    Language = link.Language,
                    Target = link.Target,
                    Text = link.Text,
                    Title = link.Title,
                };

                links.Add(linkItemDto);
            }

            return links;
        } 
    }
}
