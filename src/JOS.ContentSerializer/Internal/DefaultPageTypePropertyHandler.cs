using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultPageTypePropertyHandler : IPageTypePropertyHandler
    {
        public object GetValue(PageType pageType)
        {
            return pageType.Name;
        }
    }
}
