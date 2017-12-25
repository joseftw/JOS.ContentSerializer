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

            context.Services.AddSingleton<IPropertyHandler<int>, IntPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<bool>, BoolPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<double>, DoublePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<DateTime>, DateTimePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ContentArea>, ContentAreaPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ContentReference>, ContentReferencePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<PageReference>, PageReferencePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<ContentReference>>, ContentReferenceListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<LinkItemCollection>, LinkItemCollectionPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<PageType>, PageTypePropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<string[]>, StringArrayPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<string>, StringPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<Url>, UrlPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<XhtmlString>, XhtmlStringPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<BlockData>, BlockDataPropertyHandler>();

            context.Services.AddSingleton<IPropertyHandler<IEnumerable<string>>, StringListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<string>>, StringListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<string>>, StringListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<int>>, IntListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<int>>, IntListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<int>>, IntListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<DateTime>>, DateTimeListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<DateTime>>, DateTimeListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<DateTime>>, DateTimeListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<double>>, DoubleListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<ICollection<double>>, DoubleListPropertyHandler>();
            context.Services.AddSingleton<IPropertyHandler<IList<double>>, DoubleListPropertyHandler>();


            stopwatch.Stop();
            Trace.WriteLine($"{nameof(ContentSerializerInitalizationModule)} took {stopwatch.ElapsedMilliseconds}ms");
        }

        public void Initialize(InitializationEngine context)
        {

        }
    }
}