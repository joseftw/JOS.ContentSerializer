using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Newtonsoft.Json;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "DifferentSharedBlock", GUID = "ab8e9472-6e25-481b-a68b-cdbb21070544", Description = "")]
    [JsonObject("differentSharedBlock")]
    public class DifferentSharedBlock : BlockData
    {
        [Display(Name = "josef")]
        [JsonProperty("josef")]
        public virtual string Josef { get; set; }

        [Display(Name = "Zup")]
        [JsonProperty("zup")]
        public virtual string Subheading { get; set; }

        [Display(Name = "heading")]
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }
    }
}