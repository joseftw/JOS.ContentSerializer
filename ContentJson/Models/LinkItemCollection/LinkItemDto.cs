using Newtonsoft.Json;

namespace Jos.ContentJson.Models.LinkItemCollection
{
    public class LinkItemDto
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
