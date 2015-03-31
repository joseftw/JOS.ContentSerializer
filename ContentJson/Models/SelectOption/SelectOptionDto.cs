using Newtonsoft.Json;

namespace Jos.ContentJson.Models.SelectOption
{
    public class SelectOptionDto
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }
}
