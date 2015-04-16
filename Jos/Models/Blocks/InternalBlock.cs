using EPiServer.Core;
using EPiServer.DataAnnotations;
using Newtonsoft.Json;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "InternalBlock", GUID = "501c9536-c351-43cc-8f99-c0b34cbc307f", Description = "")]
    public class InternalBlock : BlockData
    {
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }

        [JsonProperty("summary")]
        public virtual int Summary { get; set; }
    }
}