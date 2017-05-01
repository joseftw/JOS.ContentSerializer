using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace JOS.ContentJson.Tests.Pages
{
    [ContentType(DisplayName = "StandardPage", GUID = "e48d76d2-016c-4588-9768-f68caf3a9b43", Description = "")]
    public class StandardPage : PageData
    {

        [CultureSpecific]
        [Display(
            Name = "Heading")]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Display(Name = "Description")]
        public virtual string Description { get; set; }

        [CultureSpecific]
        [Display(Name = "Age")]
        public virtual int Age { get; set; }

        [CultureSpecific]
        [Display(Name = "Starting")]
        public virtual DateTime Starting { get; set; }

        [CultureSpecific]
        [Display(Name = "Private")]
        public virtual bool Private { get; set; }

        [CultureSpecific]
        [Display(Name = "Degrees")]
        public virtual double Degrees { get; set; }

        [CultureSpecific]
        [Display(Name = "Main body")]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(Name = "Main Content Area")]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(Name = "Main Video")]
        public virtual VideoBlock MainVideo { get; set; }
    }
}