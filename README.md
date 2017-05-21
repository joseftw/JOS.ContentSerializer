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

* GlobalWrapContentAreaItems - Default = **true**
Decides if items in a contentarea should be grouped by their ContentType or not.
* UrlSettings
    * UseAbsoluteUrls - Default = **true**
    Decides if the urls returned should be absolute or relative.
    * FallbackToWildcard - Default = **true**
    If set to true, the site matched with wildcard (if any) is returned if no mapping could be found for the current hostname when using the GetByHostname method.
* ContentReferenceSettings
    * UseAbsoluteUrls - Default = **true**
    Same as for UrlSettings.
    * FallbackToWildcard - Default = **true**
    Sames as for UrlSettings.
* UseCustomPropertiesHandler - Default = **false**
Enables/Disables the CustomPropertiesHandler. Note, **NOT ENABLED BY DEFAULT.**
* ThrowOnDuplicate - Default = **false**
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
