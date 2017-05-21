using System.Collections.Generic;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IContentReferenceListPropertyHandler
    {
        IEnumerable<object> GetValue(IEnumerable<ContentReference> contentReferences);
        IEnumerable<object> GetValue(IEnumerable<ContentReference> contentReferences, ContentReferenceSettings contentReferenceSettings);
    }
}
