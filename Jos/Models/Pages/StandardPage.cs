using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace Jos.Models.Pages
{
    [ContentType(DisplayName = "StandardPage", GUID = "edd025d2-9bc4-4239-b799-9db0eb5e8aab", Description = "")]
    public class StandardPage : PageData
    {

        [CultureSpecific]
        [Display(
            Name = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Display(
            Name = "ContentArea",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        public virtual ContentArea ContentArea { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Links",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        public virtual LinkItemCollection Links { get; set; }

    }
}