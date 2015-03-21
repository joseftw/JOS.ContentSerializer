using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Newtonsoft.Json;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "InternalBlock", GUID = "07bd1b92-9ec2-4da9-909d-1c98f9624cfd", Description = "")]
    public class InternalBlock : BlockData
    {
        [Display(Name = "heading")]
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }

        [Display(Name = "Different Shared Block")]
        [JsonProperty("block")]
        public virtual DifferentSharedBlock SharedBlock { get; set; }

        [Display(Name = "Contentarea")]
        [JsonProperty("contentArea")]
        public virtual ContentArea ContentArea { get; set; }
    }
}