using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentAreaPropertyHandler
    {
        IEnumerable<IContentData> GetValue(ContentArea contentArea);
        IEnumerable<IContentData> GetValue(ContentArea contentArea, ContentSerializerSettings settings);
    }
}
