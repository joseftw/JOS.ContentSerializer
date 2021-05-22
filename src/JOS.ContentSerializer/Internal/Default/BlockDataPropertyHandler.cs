using System;
using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal.Default
{
    public class BlockDataPropertyHandler : IPropertyHandler<BlockData>
    {
        private readonly IPropertyManager _propertyManager;
        private readonly IContentSerializerSettings _contentSerializerSettings;

        public BlockDataPropertyHandler(
            IPropertyManager propertyManager,
            IContentSerializerSettings contentSerializerSettings)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
            _contentSerializerSettings = contentSerializerSettings;
        }

        public object Handle(BlockData value, PropertyInfo property, IContentData contentData)
        {
            return Handle(value, property, contentData, _contentSerializerSettings);
        }

        public object Handle(
            BlockData value,
            PropertyInfo property,
            IContentData contentData,
            IContentSerializerSettings settings)
        {
            return this._propertyManager.GetStructuredData(value, settings);
        }
    }
}
