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
using JOS.ContentSerializer.Internal.Default;
using JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers;
using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;

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
            context.Services.AddSingleton<IContentSerializer, JsonContentSerializer>();
            context.Services.AddSingleton<IPropertyManager, PropertyManager>();
            context.Services.AddSingleton<IPropertyNameStrategy, PropertyNameStrategy>();
            context.Services.AddSingleton<IPropertyResolver, PropertyResolver>();
            context.Services.AddSingleton<IUrlHelper, UrlHelperAdapter>();
            context.Services.AddSingleton<IContentJsonSerializer, JsonContentSerializer>();
            context.Services.AddSingleton<IPropertyHandlerService, PropertyHandlerService>();
            context.Services.AddSingleton<IContentSerializerSettings, ContentSerializerSettings>();

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

            var stringListPropertyHandler = new StringListPropertyHandler();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<string>>>(stringListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<ICollection<string>>>(stringListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<IList<string>>>(stringListPropertyHandler);
            var intListPropertyHandler = new IntListPropertyHandler();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<int>>>(intListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<ICollection<int>>>(intListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<IList<int>>>(intListPropertyHandler);
            var dateTimeListPropertyHandler = new DateTimeListPropertyHandler();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<DateTime>>>(dateTimeListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<ICollection<DateTime>>>(dateTimeListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<IList<DateTime>>>(dateTimeListPropertyHandler);
            var doubleListPropertyHandler = new DoubleListPropertyHandler();
            context.Services.AddSingleton<IPropertyHandler<IEnumerable<double>>>(doubleListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<ICollection<double>>>(doubleListPropertyHandler);
            context.Services.AddSingleton<IPropertyHandler<IList<double>>>(doubleListPropertyHandler);

            stopwatch.Stop();
            Trace.WriteLine($"{nameof(ContentSerializerInitalizationModule)} took {stopwatch.ElapsedMilliseconds}ms");
        }

        public void Initialize(InitializationEngine context)
        {

        }
    }
}