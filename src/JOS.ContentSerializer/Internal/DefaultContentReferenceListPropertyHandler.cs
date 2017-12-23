using System;
using System.Collections.Generic;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultContentReferenceListPropertyHandler : IPropertyHandler<IEnumerable<ContentReference>>
    {
        private readonly DefaultContentReferencePropertyHandler _defaultContentReferencePropertyHandler;
        //private readonly IContentReferencePropertyHandler _contentReferencePropertyHandler;

        //public DefaultContentReferenceListPropertyHandler(IContentReferencePropertyHandler contentReferencePropertyHandler)
        //{
        //    _contentReferencePropertyHandler = contentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(contentReferencePropertyHandler));
        //}

        public DefaultContentReferenceListPropertyHandler(DefaultContentReferencePropertyHandler defaultContentReferencePropertyHandler)
        {
            _defaultContentReferencePropertyHandler = defaultContentReferencePropertyHandler ?? throw new ArgumentNullException(nameof(defaultContentReferencePropertyHandler));
        }

        public object Handle(IEnumerable<ContentReference> value, PropertyInfo propertyInfo, IContentData contentData)
        {
            return Execute(value, propertyInfo, contentData); // TODO Allow injection of settings
        }

        private IEnumerable<object> Execute(
            IEnumerable<ContentReference> contentReferences,
            PropertyInfo property,
            IContentData contentData)
        {
            var links = new List<object>();

            foreach (var contentReference in contentReferences)
            {
                var result = this._defaultContentReferencePropertyHandler.Handle(contentReference, property, contentData);
                links.Add(result);
            }

            return links;
        }
    }
}
