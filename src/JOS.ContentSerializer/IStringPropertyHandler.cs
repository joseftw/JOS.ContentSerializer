using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    public interface IStringPropertyHandler
    {
        KeyValuePair<string, string> Handle(IContentData contentData, PropertyInfo propertyInfo);
    }
}
