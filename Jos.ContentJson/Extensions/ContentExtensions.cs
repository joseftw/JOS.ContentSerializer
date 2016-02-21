using EPiServer.Core;

namespace Jos.ContentJson.Extensions
{
    public static class ContentExtensions
    {
        public static string GetCacheKey(this IContent content)
        {
            return $"Jos.ContentJson-{content.ContentGuid}";
        }
    }
}
