using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace JOS.ContentSerializer.Tests.Pages
{
    [ContentType(DisplayName = "StringArrayPropertyHandlerPage", GUID = "414a734c-0fdf-49a5-9fc4-04d10538baa5", Description = "")]
    public class StringArrayPropertyHandlerPage : PageData
    {

        [CultureSpecific]
        [Display(
            Name = "Strings",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string[] Strings { get; set; }

    }
}