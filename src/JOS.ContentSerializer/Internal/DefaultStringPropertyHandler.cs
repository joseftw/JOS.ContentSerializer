using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultStringPropertyHandler : IStringPropertyHandler
    {
        public KeyValuePair<string, string> Handle(IContentData contentData, PropertyInfo propertyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
