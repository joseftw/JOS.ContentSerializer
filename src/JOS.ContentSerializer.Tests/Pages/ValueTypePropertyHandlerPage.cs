using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace JOS.ContentSerializer.Tests.Pages
{
    [ContentType(DisplayName = "ValueTypePropertyHandlerPage", GUID = "91b9fea0-c87e-482d-937b-0cbd5455f296", Description = "")]
    public class ValueTypePropertyHandlerPage : PageData
    {
        [CultureSpecific]
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual int Integer { get; set; }

        [CultureSpecific]
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual DateTime DateTime { get; set; }

        [CultureSpecific]
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 300)]
        public virtual double Double { get; set; }

        [CultureSpecific]
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 400)]
        public virtual bool Bool { get; set; }
    }
}