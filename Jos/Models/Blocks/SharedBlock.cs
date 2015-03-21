using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Newtonsoft.Json;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "SharedBlock", GUID = "32324ed9-fa74-404e-a7c3-a99adf438843", Description = "")]
    [JsonObject("sharedBlock")]
    public class SharedBlock : BlockData
    {
        [Display(Name = "heading")]
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }

        [Display(Name = "Subheading")]
        [JsonProperty("subHeading")]
        public virtual string Subheading { get; set; }
    }
}