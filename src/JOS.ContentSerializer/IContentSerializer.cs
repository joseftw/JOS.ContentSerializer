using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentSerializer
    {
        string Serialize(IContentData contentData);
        string Serialize(IContentData contentData, IContentSerializerSettings settings);
        object GetStructuredData(IContentData contentData);
        object GetStructuredData(IContentData contentData, IContentSerializerSettings settings);
    }
}
