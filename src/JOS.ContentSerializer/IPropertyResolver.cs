using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IPropertyResolver
    {
        IEnumerable<PropertyInfo> GetProperties(IContentData contentData);
    }
}