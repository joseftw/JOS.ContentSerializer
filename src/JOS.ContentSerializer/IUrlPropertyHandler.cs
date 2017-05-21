using EPiServer;

namespace JOS.ContentSerializer
{
    public interface IUrlPropertyHandler
    {
        object GetValue(Url url);
        object GetValue(Url url, UrlSettings urlSettings);
    }
}
