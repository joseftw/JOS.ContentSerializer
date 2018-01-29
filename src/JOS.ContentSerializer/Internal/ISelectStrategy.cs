using System.Reflection;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;

namespace JOS.ContentSerializer.Internal
{
    public interface ISelectStrategy
    {
        object Execute(PropertyInfo property, IContentData contentData, ISelectionFactory selectionFactory);
    }
}
