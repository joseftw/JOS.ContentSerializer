using EPiServer;

namespace JOS.ContentSerializer
{
    public interface IUrlPropertyHandler
    {
        string GetValue(Url url, UrlSettings urlSettings);
    }
}
