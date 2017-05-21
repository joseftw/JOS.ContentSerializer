using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentAreaPropertyHandler
    {
        IEnumerable<object> GetValue(ContentArea contentArea);
        IEnumerable<object> GetValue(ContentArea contentArea, ContentSerializerSettings settings);
    }
}
