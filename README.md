**This project were formely known as JOS.ContentJson. I've changed the name to JOS.ContentSerializer and released it as an own Nuget package.**
The old documentation for version 2 can be found [here](README.Version2.md)
# JOS.ContentSerializer

## Serialize any ContentData object to JSON(or xml, or something else)

### Installation
Nuget: **Install-Package Jos.ContentSerializer**
#### *OR*
Just check out the desired branch and add the Jos.ContentSerializer project to your solution.

### Features

Support for the most common built in properties:
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
-  ContentReferenceList

Nested ContentAreas are supported as well. When the code finds a ContentArea, it will load all items recursively so you can have multiple levels of "nesting".

### Extensible
Need support for a custom property? Maybe a Dictionary<string, string>?
It's easy to add support for custom properties.
When calling .Serialize/.ToJson you can pass in some settings. If you set ```UseCustomPropertiesHandler``` to ```true``` you will activate the ```DefaultCustomPropertiesHandler```. This is just some simple code that returns the .GetValue representation of your property. For some this may be enough, but what if you would like to only return part of that data, or something completely different?

Easy. Just implement your own CustomPropertiesHandler!
Implement ```ICustomPropertiesHandler``` and register it in the container and you should be good to go.
A custom implementation that supports ```Dictionary<string, string>``` might look like this

```
public class MyCustomPropertiesHandler : ICustomPropertiesHandler
{
    public object GetValue(object propertyValue)
    {
        switch (propertyValue)
        {
            case Dictionary<string, string> dictionary:
                // Do something here and return it!
                break;
        }
        return propertyValue;
    }
}

```
My goal when designing this library was that it should be easy to customize the result. All PropertyHandlers implements their own interfaces so if you want to change something, just make your own implementation of the interface and register it in the container.

The following interfaces exists

#### PropertyHandlers

-  IContentAreaPropertyHandler
-  IContentReferenceListPropertyHandler
-  IContentReferencePropertyHandler
-  ICustomPropertiesHandler
-  ILinkItemCollectionPropertyHandler
-  IPageTypePropertyHandler
-  IStringArrayPropertyHandler
-  IStringPropertyHandler
-  IUrlPropertyHandler
-  IValueTypePropertyHandler
-  IXhtmlStringPropertyHandler

#### Other interfaces

-  IContentJsonSerializer - The default Json serializer. Implent it and register your own if you want to replace it.
-  IContentSerializer - The default serializer. The default one is the same as IContentJsonSerializer.
-  IPropertyManager - Handles the mapping from a property to a PropertyHandler.
-  IPropertyNameStrategy - Handles how the serialized properties should be named.
-  IPropertyResolver - Handles which properties that should be serialized.
-  IUrlHelper - Handles how ContentReferences/Url-properties should be serialized.

#### IPropertyNameStrategy
The default implementation checks for the ```ContentSerializerNameAttribute``` attribute, if found, the name from the attribute will be used. If not found, the name of the property will be used.

#### IPropertyResolver
The default implementation works like this on a ContentData object.
1. It looks for the ```ContentSerializerIgnoreAttribute```, if found, the property will be skipped.
2. If no ```ContentSerializerIgnoreAttribute``` was found, it looks for the ```DisplayAttribute```, if found, the property will be included.
3. If no ```DisplayAttribute``` was found, it looks for the ```ContentSerializerIncludeAttribute```, if found, the property will be included.

### Customizable
The .Serialize and .ToJson methods both have an overload where you can pass in ```ContentSerializerSettings```.
You can change the following settings:

* GlobalWrapContentAreaItems - Default = **true**.
Decides if items in a contentarea should be grouped by their ContentType or not.
* UrlSettings
    * UseAbsoluteUrls - Default = **true**.
    Decides if the urls returned should be absolute or relative.
    * FallbackToWildcard - Default = **true**.
    If set to true, the site matched with wildcard (if any) is returned if no mapping could be found for the current hostname when using the GetByHostname method.
* ContentReferenceSettings
    * UseAbsoluteUrls - Default = **true**.
    Same as for UrlSettings.
    * FallbackToWildcard - Default = **true**.
    Sames as for UrlSettings.
* UseCustomPropertiesHandler - Default = **false**.
Enables/Disables the CustomPropertiesHandler. Note, **NOT ENABLED BY DEFAULT.**
* ThrowOnDuplicate - Default = **false**.
If the code should throw if a duplicate is added to the backing dictionary. If false, the duplicate will not be added and no exception will be thrown. When implementing your own CustomPropertiesHandler, setting this to true could be useful while developing.

### Attributes
The following attributes exists

- ContentSerializerIgnoreAttribute - If added to a property, the property will not be included in the result.
- ContentSerializerIncludeAttribute - If added to a property that doesn't have the DisplayAttribute, the property will be included in the result
- ContentSerializerNameAttribute - Makes it possible to set a custom name of the property when serialized.
- ContentSerializerWrapItemsAttribute - When used on a ContentArea, it's possible to override the global ```GlobalWrapContentAreaItems``` setting for that specific ContentArea.

### Examples

Use it like this:
```c#
    public class DemoPageController : PageController<Demopage>
    {
        public string Index(DemoPage currentPage)
        {
            var json = currentPage.ToJson();
            return json;
        }
    }
```

<details>
    <summary>DemoPage</summary>

```c#
    [ContentType(DisplayName = "DemoPage", GUID = "a6762bfb-973b-41c1-acf8-7d26567cd71d")]
    public class DemoPage : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "String",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual string String { get; set; }

        [CultureSpecific]
        [Display(
            Name = "ContentArea",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual ContentArea MainContentArea { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Degrees",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        public virtual double Degrees { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Int",
            GroupName = SystemTabNames.Content,
            Order = 400)]
        public virtual int Int { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Date",
            GroupName = SystemTabNames.Content,
            Order = 500)]
        public virtual DateTime DateTime { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Bool",
            GroupName = SystemTabNames.Content,
            Order = 600)]
        public virtual bool Bool { get; set; }

        [CultureSpecific]
        [Display(
            Name = "PageType",
            GroupName = SystemTabNames.Content,
            Order = 700)]
        public virtual PageType PageType { get; set; }

        [CultureSpecific]
        [Display(
            Name = "ContentReference",
            GroupName = SystemTabNames.Content,
            Order = 800)]
        public virtual ContentReference ContentReference { get; set; }

        [CultureSpecific]
        [Display(
            Name = "PageReference",
            GroupName = SystemTabNames.Content,
            Order = 900)]
        public virtual PageReference PageReference { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Url",
            GroupName = SystemTabNames.Content,
            Order = 1000)]
        public virtual Url Url { get; set; }

        [Display(
            Name = "InternalBlock",
            GroupName = SystemTabNames.Content,
            Order = 1100)]
        public virtual VimeoVideoBlock InternalBlock { get; set; }

        [Display(
            Name = "ContentReferenceList",
            GroupName = SystemTabNames.Content,
            Order = 1200)]
        public virtual IList<ContentReference> ContentReferenceList { get; set; }

        [Display(
            Name = "XhtmlString",
            GroupName = SystemTabNames.Content,
            Order = 1300)]
        public virtual XhtmlString XhtmlString { get; set; }

        [Display(
            Name = "LinkItemCollection",
            GroupName = SystemTabNames.Content,
            Order = 1400)]
        public virtual LinkItemCollection LinkItemCollection { get; set; }

        [Display(
            Name = "SelectOne",
            GroupName = SystemTabNames.Content,
            Order = 1500)]
        [SelectOne(SelectionFactoryType = typeof(ContactPageSelectionFactory))]
        public virtual string SelectOne { get; set; }

        [Display(
            Name = "SelectMany",
            GroupName = SystemTabNames.Content,
            Order = 1600)]
        [SelectMany(SelectionFactoryType = typeof(ContactPageSelectionFactory))]
        public virtual string SelectMany { get; set; }
    }
```
</details>
<details>
<summary>This will return a JSON representation of the `DemoPage` type like this:</summary>

```javascript
{
	"string": "This is a string",
	"mainContentArea": {
		"vimeoVideoBlock": [{
			"name": "Josef"
		}]
	},
	"degrees": 133.7,
	"int": 1337,
	"dateTime": "2017-05-18T00:00:00+02:00",
	"bool": true,
	"pageType": "DemoPage",
	"contentReference": "http://localhost:52467/about-us/management/",
	"pageReference": "http://localhost:52467/alloy-plan/download-alloy-plan/",
	"url": "http://localhost:52467/globalassets/alloy-meet/alloymeet.png",
	"internalBlock": {
		"name": "Josef is my name",
		"mainContentArea": {
			"youtubeVideoBlock": [{
				"name": "I am a youtube block"
			}]
		}
	},
	"contentReferenceList": ["http://localhost:52467/search/", "http://localhost:52467/alloy-meet/"],
	"xhtmlString": "<p>I am a xhtmlstring, do you like it?</p>\n<p>Im <strong>bold not <em>bald</em></strong></p>",
	"linkItemCollection": [{
		"href": "http://localhost:52467/alloy-plan/?query=any",
		"title": "Josef Ottossons sida",
		"target": "_blank",
		"text": "Josef Ottosson"
	}, {
		"href": "https://josef.guru",
		"title": "External link",
		"target": "_blank",
		"text": "External"
	}, {
		"href": "mailto:i@josef.guru",
		"title": "Email link",
		"target": null,
		"text": "Email"
	}],
	"selectOne": [{
		"selected": false,
		"text": "Amar Gupta",
		"value": "34"
	}, {
		"selected": false,
		"text": "Fiona Miller",
		"value": "33"
	}, {
		"selected": true,
		"text": "Michelle Hernandez",
		"value": "30"
	}, {
		"selected": false,
		"text": "Robert Carlsson",
		"value": "32"
	}, {
		"selected": false,
		"text": "Todd Slayton",
		"value": "31"
	}],
	"selectMany": [{
		"selected": false,
		"text": "Amar Gupta",
		"value": "34"
	}, {
		"selected": true,
		"text": "Fiona Miller",
		"value": "33"
	}, {
		"selected": false,
		"text": "Michelle Hernandez",
		"value": "30"
	}, {
		"selected": false,
		"text": "Robert Carlsson",
		"value": "32"
	}, {
		"selected": false,
		"text": "Todd Slayton",
		"value": "31"
	}]
}
```
</details>
