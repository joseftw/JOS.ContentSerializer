using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;

namespace JOS.ContentSerializer.Internal
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ContentSerializerInitalizationModule : IConfigurableModule
    {
        public void Uninitialize(InitializationEngine context)
        {

        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IContentSerializer, DefaultJsonContentSerializer>();
            context.Services.AddSingleton<IPropertyManager, PropertyManager>();
            context.Services.AddSingleton<IPropertyNameStrategy, DefaultPropertyNameStrategy>();
            context.Services.AddSingleton<IPropertyResolver, DefaultPropertyResolver>();
            context.Services.AddSingleton<IUrlHelper, UrlHelperAdapter>();
            context.Services.AddSingleton<IContentJsonSerializer, DefaultJsonContentSerializer>();
            context.Services.AddSingleton<IPropertyHandlerService, DefaultPropertyHandlerService>();
            var defaultPropertyHandlerScanner = new DefaultPropertyHandlerScanner();
            defaultPropertyHandlerScanner.Scan();
            context.Services.AddSingleton<IPropertyHandlerScanner>(defaultPropertyHandlerScanner);

            context.Services.AddSingleton<IPropertyHandler<int>, DefaultIntPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<bool>, DefaultBoolPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<double>, DefaultDoublePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<DateTime>, DefaultDateTimePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ContentArea>, DefaultContentAreaPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ContentReference>, DefaultContentReferencePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<ContentReference>>, DefaultContentReferenceListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<LinkItemCollection>, DefaultLinkItemCollectionPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<PageType>, DefaultPageTypePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<string[]>, DefaultStringArrayPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<string>, DefaultStringPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<Url>, DefaultUrlPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<XhtmlString>, DefaultXhtmlStringPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<BlockData>, DefaultBlockDataPropertyHandler>();
        }

        public void Initialize(InitializationEngine context)
        {

        }
    }
}