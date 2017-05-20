using System;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

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
            context.Services.AddSingleton<IContentAreaPropertyHandler, DefaultContentAreaPropertyHandler>();
            context.Services.AddSingleton<IContentDataPropertyHandler, DefaultContentDataPropertyHandler>();
            context.Services.AddSingleton<IContentSerializer, DefaultJsonContentSerializer>();
            context.Services.AddSingleton<IPropertyManager, PropertyManager>();
            context.Services.AddSingleton<IPropertyNameStrategy, DefaultPropertyNameStrategy>();
            context.Services.AddSingleton<IPropertyResolver, DefaultPropertyResolver>();
            context.Services.AddSingleton<IStringArrayPropertyHandler, DefaultStringArrayPropertyHandler>();
            context.Services.AddSingleton<IStringPropertyHandler, DefaultStringPropertyHandler>();
            context.Services.AddSingleton<IUrlHelper, UrlHelperAdapter>();
            context.Services.AddSingleton<IUrlPropertyHandler, DefaultUrlPropertyHandler>();
            context.Services.AddSingleton<IValueTypePropertyHandler, DefaultValueTypePropertyHandler>();
            context.Services.AddSingleton<IContentReferencePropertyHandler, DefaultContentReferencePropertyHandler>();
            context.Services.AddSingleton<IPageTypePropertyHandler, DefaultPageTypePropertyHandler>();
            context.Services
                .AddSingleton<IContentReferenceListPropertyHandler, DefaultContentReferenceListPropertyHandler>();
            context.Services.AddSingleton<IXhtmlStringPropertyHandler, DefaultXhtmlStringPropertyHandler>();
        }

        public void Initialize(InitializationEngine context)
        {

        }
    }
}