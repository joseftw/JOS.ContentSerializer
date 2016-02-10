# EPiServer-ContentData-ToJson
Converts any ContentData object to JSON 

We needed to get a JSON representation of EPiServer contenttypes at work because of 
how our frontend-framework(uses backbone and stuff) works.

It currently supports the following EPiServer Property Types(more to come!):

-  String
-  bool
-  XhtmlString
-  ContentArea(*different ContentTypes as well!*)
-  InternalBlock
-  Double
-  Int
-  DateTime
-  SelectOne
-  SelectMany
-  PageReference
-  ContentReference
-  LinkItemCollection
-  Url

Following Branches exists:

Master - Will always support the latest EPi

Develop - Bleeding edge but sometimes not stable

EPi8 - Support for EPi 8.0 +

###Installation###
Nuget: **Install-Package Jos.ContentJson** *Note, this is the master branch.* (My first Nuget package, please tell me if something's wrong)
####*OR*####
Just check out the desired branch and add the Jos.ContentJson project to your solution.

###Examples###

Use it like this:
```c#
    public class StartpageController : PageController<Startpage>
    {
        public string Index(Startpage currentPage)
        {
            var json = currentPage.ToJson();
            return json;
        }
    }
```
This will return a JSON representation of the `Startpage` type like this:
````javascript
    {
        "heading": "This is the heading",
        "body": "<p>This is some text, its cool because <strong>JOSEF WROTE IT</strong></p>",
        "endDate": "2015-03-25T00:00:00",
        "price": 13.37,
        "thisIsSweet": true
    }
````    
**Startpage**:
```c#
    [ContentType(DisplayName = "Startpage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d")]
    public class Startpage : PageData
    {
        [Display(Name = "Heading", Order = 100)]
        public virtual string Heading { get; set; }

        [Display(Name = "Body", Order = 110)]
        public virtual XhtmlString Body { get; set; }
        
        [Display(Name = "End Date", Order = 130)]
        public virtual DateTime EndDate { get; set; }

        [Display(Name = "Price", Order = 140)]
        public virtual Double Price { get; set; }

        [Display(Name = "This is Sweet", Order = 150)]
        public virtual bool ThisIsSweet { get; set; }
    }
```    
~~Each property that you want to include in the JSON response needs to be decorated with the JsonProperty attribute(Json.net...).
*Im thinking about making this optional(if you want to specify a custom JSON key for example) and instead select all properties
that has the Display attribute...*~~

This is now implemented. All properties with the `Display`-attribute will appear in the JSON. You can still use the `JsonProperty`-attribute to define custom JSON-keys and/or add properties that doesn't have the `Display`-attribute. You can also use the `JsonIgnore`-attribute on properties you don't want appearing in the JSON.

Anyhow, that example was pretty basic, what about internal blocks?

####Internal Blocks####

**Startpage**:
```c#
    [ContentType(DisplayName = "Startpage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d", Description = "")]
    public class Startpage : PageData
    {
        [Display(Name = "Heading", Order = 100)]
        public virtual string Heading { get; set; }

        [Display(Name = "ThisIsSweet", Order = 150)]
        public virtual bool ThisIsSweet { get; set; }

        [Display(Name = "InternalBlock", Order = 160)]
        public virtual InternalBlock InternalBlock { get; set; }
    }
```
**InternalBlock**:
```c#
    [ContentType(DisplayName = "InternalBlock", GUID = "07bd1b92-9ec2-4da9-909d-1c98f9624cfd", Description = "")]
    public class InternalBlock : BlockData
    {
        [Display(Name = "heading")]
        public virtual string Heading { get; set; }
        
        [Display(Name = "greatestRapperAlive")]
        public virtual string GreatestRapperAlive { get; set; }
    }
  ```  
  The JSON response would look like this:
  ```javascript
    {
        "heading": "This is pretty Cool",
        "thisIsSweet": true,
        "internalBlock": {
            "heading": "Internal JOOOSEF",
            "greatestRapperAlive": "Eminem"
        }
    }
```
####Contentarea####

Now I've added a ContentArea to my Startpage like this:
```c#
    [ContentType(DisplayName = "Startpage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d", Description = "")]
    public class Startpage : PageData
    {
        [Display(Name = "Heading")]
        public virtual string Heading { get; set; }

        [Display(Name = "ThisIsSweet")]
        public virtual bool ThisIsSweet { get; set; }

        [Display(Name = "InternalBlock")]
        public virtual InternalBlock InternalBlock { get; set; }
        
        [Display(Name = "Contentarea")]
        public virtual ContentArea ContentArea { get; set; }
    }
``` 
When adding a couple of InternalBlocks to the ContentArea the JSON response would look like this:
```javascript
    {
        "heading": "This is pretty cool",
        "thisIsSweet": true,
        "internalBlock": {
            "heading": "This is the Heading",
            "greatestRapperAlive": "Eminem"
        },
        "contentArea": {
                "internalBlock": [
                {
                    "heading": "Im in a ContentArea",
                    "greatestRapperAlive": "Biggie"
                },
                {
                    "heading": "Me too",
                    "greatestRapperAlive": "Tupac"
                }
            ]
        }
    }
```
As you can see the blocks get placed in an array under the property internalBlock.
Why is that? Well this is to support different ContentTypes in the ContentArea. What would happen if we added the BlockType 
`DifferentBlock` to our ContentArea?
```c#
    [ContentType(DisplayName = "DifferentBlock", GUID = "18bd1b92-9ec2-4da9-909d-1c98f9624cfe", Description = "")]
    public class DifferentBlock : BlockData
    {
        [Display(Name = "Worst Rapper Alive")]
        public virtual string WorstRapperAlive { get; set; }
        
        [Display(Name = "Worst rapper ever?")]
        public virtual bool WorstRapperEver { get; set; }
    }
```
The JSON response would look like this if we added one `DifferentBlock` to our ContentArea:
```javascript
    {
        "heading": "This is pretty cool",
        "thisIsSweet": true,
        "internalBlock": {
            "heading": "This is the Heading",
            "greatestRapperAlive": "Eminem"
        },
         "contentArea": {
             "internalBlock": [
                {
                    "heading": "Im in a ContentArea",
                    "greatestRapperAlive": "Biggie(will never die)"
                },
                {
                    "heading": "Me too",
                    "greatestRapperAlive": "Tupac(will never die)"
                }
            ],
            "differentBlock": [
                {
                    "worstRapperAlive": "Drake",
                    "worstRapperEver": false
                },
                {
                    "worstRapperAlive": "Flo Rida",
                    "worstRapperEver": true
                }
            ]
        }
    }
```
#####Custom JSON key#####
If you want to give the Items in your ContentArea a different JSON key you could decorate your class with the JsonObject attribute like this:
```c#
    [ContentType(DisplayName = "DifferentBlock", GUID = "18bd1b92-9ec2-4da9-909d-1c98f9624cfe", Description = "")]
    [JsonObject("customJsonKey")]
    public class DifferentBlock : BlockData
    {
        [Display(Name = "Worst Rapper Alive")]
        public virtual string WorstRapperAlive { get; set; }
        
        [Display(Name = "Worst rapper ever?")]
        public virtual bool WorstRapperEver { get; set; }
    }
```
The JSON would now look like this:
```javascript
    {
        "heading": "This is pretty cool",
        "thisIsSweet": true,
        "internalBlock": {
            "heading": "This is the Heading",
            "greatestRapperAlive": "Eminem"
        },
         "contentArea": {
             "internalBlock": [
                {
                    "heading": "Im in a ContentArea",
                    "greatestRapperAlive": "Biggie(will never die)"
                },
                {
                    "heading": "Me too",
                    "greatestRapperAlive": "Tupac(will never die)"
                }
            ],
            "customJsonKey": [
                {
                    "worstRapperAlive": "Drake",
                    "worstRapperEver": false
                },
                {
                    "worstRapperAlive": "Flo Rida",
                    "worstRapperEver": true
                }
            ]
        }
    }
```
There is, of course :), support for nested ContentAreas and Internal Blocks etc...here's an JSON example:
```javascript
    {
        "heading": "This is Pretty cool",
        "thisIsSweet": true,
        "internalBlock": {
            "heading": "This is the Heading",
            "greatestRapperAlive": "Eminem",
            "differentBlock": {
                "one": "This is",
                "two": "The Sound of",
                "three": "The POLICE!!!"
            },
            "contentArea": {
                "internalBlock": [
                    {
                        "heading": "Damn Im deep...",
                        "greatestRapperAlive": "Biggie(will never die)"
                    },
                    {
                        "heading": "Me too bro",
                        "greatestRapperAlive": "Tupac(will never die)"
                    }
                ],
                "differentBlock": [
                    {
                        "worstRapperAlive": "Drake",
                        "worstRapperEver": false
                    },
                    {
                        "worstRapperAlive": "Flo Rida",
                        "worstRapperEver": true
                    }
                ]
            }
        },
        "contentArea": {
            "differentBlock": [
                {
                    "one": "Sound",
                    "two": "of the",
                    "three": "Police!"
                }
            ],
            "sharedBlock": [
                {
                    "heading": "HEJ",
                    "subHeading": "JOSEF"
                }
            ]
        }
    }
```

#####Custom JSON keys#####
If you want to specify a custom JSON key to a property, simply add a ```JsonProperty```-attribute like this
```c#
[Display(Name = "Worst Rapper Alive")]
[JsonProperty("myCustomJsonKey")]
public virtual string WorstRapperAlive { get; set; }
```

#####Ignore properties#####
If you have a property that you don't want to appear in the JSON, simply add a ```JsonIgnore```-attribute like this
```c#
[Display(Name = "Worst Rapper Alive")]
[JsonIgnore]
public virtual string WorstRapperAlive { get; set; }
```

#####Wrapping of items in ContentArea#####
When calling ToJson on a ContentArea it's possible to NOT wrap the items. 
Example(wrapped):
```c#
var json = currentPage.ContentArea.ToJson();
```
Would produce this JSON:
```javascript
{
    "youTubeBlock": [
        {
            "heading": "Heading",
            "info": "This is some info text",
            "youTubeId": "489ujksankf833"
        }
    ],
    "textAndImageBlock": [
        {
            "text": "Some Text here",
            "image": null
        }
    ],
    "contentAreaBlock": [
        {
            "contentArea": {
                "youTubeBlock": [
                    {
                        "heading": "Heading",
                        "info": "Info text",
                        "youTubeId": "8592jmklff4252"
                    }
                ]
            }
        }
    ],
    "overridenBlockWithInternalBlockName": [
        {
            "name": "Hey hey",
            "overridenInternalBlockName": {
                "heading": "Heading",
                "summary": 4
            }
        }
    ]
}
```

And this is how the unwrapped JSON would look
```c#
var json = currentPage.ContentArea.ToJson(false);
```
Gives:
```javascript
[
    {
        "heading": "Heading",
        "info": "This is some info text",
        "youTubeId": "489ujksankf833"
    },
    {
        "text": "Some Text here",
        "image": null
    },
    {
        "contentArea": {
            "youTubeBlock": [
                {
                    "heading": "Heading",
                    "info": "Info text",
                    "youTubeId": "8592jmklff4252"
                }
            ]
        }
    },
    {
        "name": "Hey hey",
        "overridenInternalBlockName": {
            "heading": "Heading",
            "summary": 4
        }
    }
]
```


####Extension methods####
The following extensions methods are provided:
* **ContentArea**
 * ToJson - returns a JSON representation of the ContentArea
 * GetStructuredDictionary - returns a Dictionary representation of the ContentArea
* **ContentData**
 * ToJson - returns a JSON representation of the ContentData
 * GetStructuredDictionary - returns a Dictionary representation of the ContentData
* **ContentReference**
 * ToPrettyUrl - returns a pretty url to the contentreference, supports absolute and relative
* **Url**
 * ToPrettyUrl - returns a pretty url from a Url property. Supports mailto as well. 
