using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IPropertyManager
    {
        Dictionary<string, object> GetStructuredData(
            IContentData contentData,
            ContentSerializerSettings contentSerializerSettings);
    }
}
