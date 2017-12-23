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
            context.Services.AddSingleton<IContentSerializer, DefaultJsonContentSerializer>();
            context.Services.AddSingleton<IPropertyManager, PropertyManager>();
            context.Services.AddSingleton<IPropertyNameStrategy, DefaultPropertyNameStrategy>();
            context.Services.AddSingleton<IPropertyResolver, DefaultPropertyResolver>();
            context.Services.AddSingleton<IUrlHelper, UrlHelperAdapter>();
            context.Services.AddSingleton<IContentJsonSerializer, DefaultJsonContentSerializer>();
        }

        public void Initialize(InitializationEngine context)
        {

        }
    }
}