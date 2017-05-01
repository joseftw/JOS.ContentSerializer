using System;
using EPiServer.Core;
using JOS.ContentJson.Core;

namespace JOS.ContentJson
{
    public class ContentJson
    {
        private readonly IContentJsonHandler _contentJsonHandler;

        public ContentJson(IContentJsonHandler contentJsonHandler)
        {
            _contentJsonHandler = contentJsonHandler ?? throw new ArgumentNullException(nameof(contentJsonHandler));
        }

        public string ToJson(IContentData contentData)
        {
            return this._contentJsonHandler.Execute(contentData);
        }
    }
}
