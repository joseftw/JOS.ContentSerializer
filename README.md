# EPiServer-ContentData-ToJson
Converts any ContentData object to JSON 

It currently supports the following EPiServer Property Types(more to come!):

-  String
-  bool
-  XhtmlString
-  ContentArea
-  InternalBlock
-  Double
-  Int
-  DateTime

We needed to get a JSON representation of EPiServer contenttypes at work because of 
how our frontend-framework(uses backbone and stuff) works.

I decided to create an extensionmethod to the ContentData type, it can be used like this:

    public class StartpageController : PageController<Startpage>
    {
        public string Index(Startpage currentPage)
        {
            var json = currentPage.ToJson();
            return json;
        }
    }

This will return a JSON representation of the `Startpage` type like this:

    {
        "heading": "Joooosef",
        "body": "<p>This is some text, its cool because <strong>JOSEF WROTE IT</strong></p>",
        "endDate": "2015-03-25T00:00:00",
        "price": 13.37,
        "thisIsSweet": true
    }
    
And this is how the Startpage class looks like:

    [ContentType(DisplayName = "Startpage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d")]
    public class Startpage : PageData
    {
        [Display(Name = "Heading", Order = 100)]
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }

        [Display(Name = "Body", Order = 110)]
        [JsonProperty("body")]
        public virtual XhtmlString Body { get; set; }
        
        [Display(Name = "End Date", Order = 130)]
        [JsonProperty("endDate")]
        public virtual DateTime EndDate { get; set; }

        [Display(Name = "Price", Order = 140)]
        [JsonProperty("price")]
        public virtual Double Price { get; set; }

        [Display(Name = "This is Sweet", Order = 150)]
        [JsonProperty("thisIsSweet")]
        public virtual bool ThisIsSweet { get; set; }
    }
    
Each property that you want to include in the JSON response needs to be decorated with the JsonProperty attribute(Json.net...).
Im thinking about making this optional(if you want to specify a custom JSON key for example) and instead select all properties
that has the Display attribute...

Anyhow, that example was pretty basic, what about internal blocks?

####Internal Blocks####

My **Startpage** now looks like this:

    [ContentType(DisplayName = "Startpage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d", Description = "")]
    public class Startpage : PageData
    {
        [Display(Name = "Heading", Order = 100)]
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }

        [Display(Name = "bool", Order = 150)]
        [JsonProperty("bool")]
        public virtual bool Bool { get; set; }

        [Display(Name = "InternalBlock", Order = 160)]
        [JsonProperty("internalBlock")]
        public virtual InternalBlock InternalBlock { get; set; }
    }

And the InternalBlock looks like this:

    [ContentType(DisplayName = "InternalBlock", GUID = "07bd1b92-9ec2-4da9-909d-1c98f9624cfd", Description = "")]
    public class InternalBlock : BlockData
    {
        [Display(Name = "heading")]
        [JsonProperty("heading")]
        public virtual string Heading { get; set; }
        
        [Display(Name = "heading")]
        [JsonProperty("greatestRapperAlive")]
        public virtual string GreatestRapperAlive { get; set; }
    }
    
  The JSON response would look like this:
  
  
