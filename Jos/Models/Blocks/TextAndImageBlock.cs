using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Newtonsoft.Json;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "TextAndImageBlock", GUID = "8c76306a-b216-42a8-96e8-e09c083dd538", Description = "")]
    public class TextAndImageBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Text",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Text { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Image",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [UIHint(UIHint.Image)]
        [JsonProperty("image")]
        public virtual ContentReference Image { get; set; }

    }
}