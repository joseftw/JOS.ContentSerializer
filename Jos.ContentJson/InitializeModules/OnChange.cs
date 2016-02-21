using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Jos.ContentJson.Extensions;

namespace Jos.ContentJson.InitializeModules
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class OnChange : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var events = ServiceLocator.Current.GetInstance<IContentEvents>();
            events.PublishedContent += this.UpdateContentJsonCache;
            events.DeletedContent += this.DeleteContentJsonCache;
        }

        public void Uninitialize(InitializationEngine context)
        {
            var events = ServiceLocator.Current.GetInstance<IContentEvents>();
            events.PublishedContent -= this.UpdateContentJsonCache;
            events.DeletedContent -= this.DeleteContentJsonCache;
        }

        private void UpdateContentJsonCache(object sender, ContentEventArgs e)
        {
            var josContentJsonKey = e.Content.GetCacheKey();
            try
            {
                var json = e.Content.ToJson(cached: false);
                CacheManager.Insert(josContentJsonKey, json);
            }
            catch (Exception ex)
            {
                //TODO: Add logging support.
            }            
        }

        private void DeleteContentJsonCache(object sender, ContentEventArgs e)
        {
            var josContentJsonKey = e.Content.GetCacheKey();
            CacheManager.Remove(josContentJsonKey);
        }
    }
}
