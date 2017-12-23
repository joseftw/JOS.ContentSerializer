using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentReferenceListPropertyHandler : PropertyHandler<IEnumerable<ContentReference>>
    {
        private readonly IContentReferencePropertyHandler _contentReferencePropertyHandler;

        public DefaultContentReferenceListPropertyHandler(IContentReferencePropertyHandler contentReferencePropertyHandler)
        {
            _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        }

        public override object Handle(object value, PropertyInfo propertyInfo, IContentData contentData)
        {
            var contentReferences = (IEnumerable<ContentReference>)value;
            return Execute(contentReferences, new ContentReferenceSettings()); // TODO Allow injection of settings
        }

        private IEnumerable<object> Execute(
            IEnumerable<ContentReference> contentReferences,
            ContentReferenceSettings contentReferenceSettings)
        {
            var links = new List<object>();

            foreach (var contentReference in contentReferences)
            {
                var result = this._contentReferencePropertyHandler.GetValue(contentReference, contentReferenceSettings);
                links.Add(result);
            }

            return links;
        }
    }
}
