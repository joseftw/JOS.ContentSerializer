using System.Collections.Generic;
using EPiServer.SpecializedProperties;

namespace JOS.ContentSerializer
{
    public interface ILinkItemCollectionPropertyHandler
    {
        IEnumerable<object> GetValue(LinkItemCollection linkItemCollection);
        IEnumerable<object> GetValue(LinkItemCollection linkItemCollection, UrlSettings contentReferenceSettings);
    }
}
