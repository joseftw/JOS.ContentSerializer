using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IPropertyManager // TODO Do we really need this?
    {
        Dictionary<string, object> GetStructuredData(
            IContentData contentData,
            ContentSerializerSettings contentSerializerSettings);
    }
}
