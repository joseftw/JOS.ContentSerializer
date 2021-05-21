# JOS.ContentSerializer - Converts any Episerver ContentData object to JSON

## Serialize any ContentData object to JSON(or xml, or something else)

### Installation
Nuget: **Install-Package Jos.ContentSerializer**
#### *OR*
Just check out the desired branch and add the Jos.ContentSerializer project to your solution.

### Features

Support for the most common built in properties:
* ```BlockData```
* ```bool```
* ```double```
* ```ContentArea```
* ```ContentReference```
* ```DateTime```
* ```IEnumerable<ContentReference>```
* ```IEnumerable<string>``` *ICollection/IList works as well*
* ```IEnumerable<int>``` *ICollection/IList works as well*
* ```IEnumerable<double>``` *ICollection/IList works as well*
* ```IEnumerable<DateTime>``` *ICollection/IList works as well*
* ```int```
* ```LinkItemCollection```
* ```PageReference```
* ```PageType```
* ```string[]```
* ```string``` *SelectOne/SelectMany* support as well.*
* ```Url```
* ```XhtmlString```

Nested ContentAreas are supported as well. When the code finds a ContentArea, it will load all items recursively so you can have multiple levels of "nesting".

### Extensible, really easy to add support for custom properties
Example:
You're using the property [Jos.PropertyKeyValueList](https://github.com/joseftw/JOS.PropertyKeyValueList) on your StartPage like this.

```csharp
public class StartPage : PageData
{
    ...
    public virtual string Heading { get; set; }
    public virtual IEnumerable<KeyValueItem> KeyValueItems{ get; set; }
    ...
}
```
Now, if you call .ToJson on a StartPage instance you would only get the Heading property in the json output since ```IEnumerable<KeyValueItem>``` isn't handled out of the box.
```javascript
{
    "heading" : "Where is my KeyValueItems??"
}
```

To add support for it, first create a new class that implements the ```IPropertyHandler2<>``` interface

```csharp
public class KeyValueItemListPropertyHandler : IPropertyHandler2<IEnumerable<KeyValueItem>>
{
    public object Handle2(IEnumerable<KeyValueItem> value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
    {
        // Do whatever you want with the property here.
        return value;
    }
}
```
Then register your class in your DI container.
**Example**
```csharp
[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class MyConfigurationModule : IConfigurableModule
{
    public void ConfigureContainer(ServiceConfigurationContext context)
    { 
    context.Services.AddSingleton<IPropertyHandler2<IEnumerable<KeyValueItem>>, KeyValueItemListPropertyHandler>();
    }
}
```
Now, if you would call .ToJson again on your StartPage instance, you would see the following output.

```javascript
{
    "heading": "Where is my KeyValueItems??",
    "keyValueItems": [
        {
            "key": "Some key",
            "value": "Hello there"
        },
        {
            "key": "Another key",
            "value": "Another value!"
        }
    ]
}
```

### Extend/Replace built in PropertyHandlers
Say that you, for some reason, want all strings to return "JOSEF OTTOSSON!!" instead of their actual value.

Just create a new propertyhandler for strings like this.
```csharp
public class JosefStringPropertyHandler : IPropertyHandler2<string>
{
    public object Handle2(string value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings)
    {
        return "JOSEF OTTOSSON!!";
    }
}
```

Then swap out the default ```StringPropertyHandler``` in the DI container like this:
```csharp
context.Services.AddSingleton<IPropertyHandler2<string>, JosefStringPropertyHandler>();
```
**Don't forget to unregister the default ``IPropertyHandler<string>`` as well**

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

### Attributes
The following attributes exists

- ContentSerializerIgnoreAttribute - If added to a property, the property will not be included in the result.
- ContentSerializerIncludeAttribute - If added to a property that doesn't have the DisplayAttribute, the property will be included in the result
- ContentSerializerNameAttribute - Makes it possible to set a custom name of the property when serialized.
- ContentSerializerWrapItemsAttribute - When used on a ContentArea, it's possible to override the global ```GlobalWrapContentAreaItems``` setting for that specific ContentArea.
- ContentSerializerPropertyHandlerAttribute - Makes it possible to specify a custom PropertyHandler for a specific property.

### Examples

Use it like this:
```c#
public class DemoPageController : PageController<Demopage>
{
    public string Index(DemoPage currentPage)
    {
        return currentPage.ToJson();
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
		"href": "mailto:mail@example.com",
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
