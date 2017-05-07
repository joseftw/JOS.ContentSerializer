using EPiServer;

namespace JOS.ContentSerializer
{
    public interface IUrlPropertyHandler
    {
        string GetValue(Url url, bool absoluteUrl);
        string GetValue(Url url, string baseUrl, bool absoluteUrl);
    }
}
