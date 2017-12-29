using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class BlockDataPropertyHandler : IPropertyHandler<BlockData>
    {
        private readonly IPropertyManager _propertyManager;
        private readonly ContentSerializerSettings _contentSerializerSettings;

        public BlockDataPropertyHandler(IPropertyManager propertyManager, ContentSerializerSettings contentSerializerSettings)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
            _contentSerializerSettings = contentSerializerSettings;
        }

        public object Handle(BlockData value, PropertyInfo property, IContentData contentData)
        {
            return this._propertyManager.GetStructuredData(value, this._contentSerializerSettings);
        }
    }
}
