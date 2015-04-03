using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "ContentAreaBlock", GUID = "34a3eeac-cec6-41a7-a6cd-2b999b2baaa6", Description = "")]
    public class ContentAreaBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Name",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea ContentArea { get; set; }

    }
}