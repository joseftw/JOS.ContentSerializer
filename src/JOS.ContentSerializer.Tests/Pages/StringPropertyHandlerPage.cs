using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;

namespace JOS.ContentSerializer.Tests.Pages
{
    [ContentType(DisplayName = "StringPropertyHandlerPage", GUID = "e3a74971-d641-4081-95e0-b0200284d8cd", Description = "")]
    public class StringPropertyHandlerPage : PageData
    {

        [CultureSpecific]
        [Display(
            Name = "Main body",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Heading { get; set; }

        [SelectOne(SelectionFactoryType = typeof(SelectionFactory))]
        public virtual string SelectOne { get; set; }

        [SelectMany(SelectionFactoryType = typeof(SelectionFactory))]
        public virtual string SelectMany { get; set; }

        [SelectOne(SelectionFactoryType = typeof(SelectionFactory))]
        public virtual string SelectedOnlyOne { get; set; }

        [SelectMany(SelectionFactoryType = typeof(SelectionFactory))]
        public virtual string SelectedOnlyMany { get; set; }

        [SelectOne(SelectionFactoryType = typeof(SelectionFactory))]
        public virtual string SelectedOnlyValueOnlyOne { get; set; }

        [SelectMany(SelectionFactoryType = typeof(SelectionFactory))]
        public virtual string SelectedOnlyValueOnlyMany { get; set; }
    }
}