using System;
using System.Collections.Generic;
using System.Diagnostics;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using JOS.ContentSerializer.Internal.ValueListPropertyHandlers;
using JOS.ContentSerializer.Internal.ValueTypePropertyHandlers;

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
            var stopwatch = new Stopwatch();
            stopwatch.Start();
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
            context.Services.AddSingleton<IPropertyHandler<PageReference>, DefaultPageReferencePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<ContentReference>>, DefaultContentReferenceListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<LinkItemCollection>, DefaultLinkItemCollectionPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<PageType>, DefaultPageTypePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<string[]>, DefaultStringArrayPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<string>, DefaultStringPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<Url>, DefaultUrlPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<XhtmlString>, DefaultXhtmlStringPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<BlockData>, DefaultBlockDataPropertyHandler>();

            context.Services.AddSingleton<IPropertyHandler<IEnumerable<string>>, DefaultStringListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<string>>, DefaultStringListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<string>>, DefaultStringListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<int>>, DefaultIntListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<int>>, DefaultIntListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<int>>, DefaultIntListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<DateTime>>, DefaultDateTimeListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<DateTime>>, DefaultDateTimeListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<DateTime>>, DefaultDateTimeListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<double>>, DefaultDoubleListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<double>>, DefaultDoubleListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<double>>, DefaultDoubleListPropertyHandler>();


            stopwatch.Stop();
            Trace.WriteLine($"{nameof(ContentSerializerInitalizationModule)} took {stopwatch.ElapsedMilliseconds}ms");
        }

        public void Initialize(InitializationEngine context)
        {

        }
    }
}