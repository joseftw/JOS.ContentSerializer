using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Jos.Models.Blocks;
using Newtonsoft.Json;

namespace Jos.Models.Pages
{
    [ContentType(DisplayName = "Startpage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d", Description = "")]
    public class Startpage : PageData
    {

        [CultureSpecific]
        [Display(Name = "String", Order = 100)]
        [JsonProperty("string")]
        public virtual string Heading { get; set; }

        [CultureSpecific]
        [Display(Name = "XhtmlString", Order = 110)]
        [JsonProperty("xhtmlString")]
        public virtual XhtmlString HtmlText { get; set; }

        [CultureSpecific]
        [Display(Name = "Int", Order = 120)]
        [JsonProperty("integer")]
        public virtual int Integer { get; set; }

        [CultureSpecific]
        [Display(Name = "DateTime", Order = 130)]
        [JsonProperty("dateTime")]
        public virtual DateTime DateTime { get; set; }

        [CultureSpecific]
        [Display(Name = "Double", Order = 140)]
        [JsonProperty("double")]
        public virtual Double Double { get; set; }

        [Display(Name = "bool", Order = 150)]
        [JsonProperty("bool")]
        public virtual bool Bool { get; set; }

        [Display(Name = "InternalBlock", Order = 160)]
        [JsonProperty("internalBlock")]
        public virtual InternalBlock InternalBlock { get; set; }

        [CultureSpecific]
        [Display(Name = "Contentarea", Order = 170)]
        [JsonProperty("contentArea")]
        public virtual ContentArea ContentArea { get; set; }

    }
}