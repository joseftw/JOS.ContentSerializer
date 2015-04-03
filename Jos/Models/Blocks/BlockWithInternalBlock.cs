using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Newtonsoft.Json;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "BlockWithInternalBlock", GUID = "626baf68-1903-443f-93a1-57f7afa1c3ce", Description = "")]
    [JsonObject("overridenBlockWithInternalBlockName")]
    public class BlockWithInternalBlock : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Name",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Name { get; set; }

        [Display(
            Name = "Internal Block",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        [JsonProperty("overridenInternalBlockName")]
        public virtual InternalBlock InternalBlock { get; set; }

    }
}