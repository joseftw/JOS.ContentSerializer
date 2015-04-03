using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Jos.Models.Blocks
{
    [ContentType(DisplayName = "YouTubeBlock", GUID = "3f663a5a-d3cd-4730-8f9c-7fd99e63efc8", Description = "")]
    public class YouTubeBlock : BlockData
    {

        [CultureSpecific]
        [Display(
            Name = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Info",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual string Info { get; set; }

        [CultureSpecific]
        [Display(
            Name = "YouTube ID",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual string YouTubeId { get; set; }

    }
}