using EPiServer.DataAbstraction;

namespace JOS.ContentSerializer
{
    public interface IPageTypePropertyHandler
    {
        object GetValue(PageType pageType);
    }
}