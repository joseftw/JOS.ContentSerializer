using System;
using System.Reflection;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;

namespace JOS.ContentSerializer.Internal.Default
{
    public class StringPropertyHandler : IPropertyHandler<string>
    {
        private readonly ISelectOneStrategy _selectOneStrategy;
        private readonly ISelectManyStrategy _selectManyStrategy;

        public StringPropertyHandler(ISelectOneStrategy selectOneStrategy, ISelectManyStrategy selectManyStrategy)
        {
            _selectOneStrategy = selectOneStrategy ?? throw new ArgumentNullException(nameof(selectOneStrategy));
            _selectManyStrategy = selectManyStrategy ?? throw new ArgumentNullException(nameof(selectManyStrategy));
        }

        public object Handle(string stringValue, PropertyInfo property, IContentData contentData,
            IContentSerializerSettings contentSerializerSettings)
        {
            if (HasSelectAttribute(property))
            {
                var selectOneAttribute = GetSelectOneAttribute(property);
                if (selectOneAttribute != null)
                {
                    var selectionFactory = CreateSelectionFactoryInstance(selectOneAttribute.SelectionFactoryType);
                    return this._selectOneStrategy.Execute(property, contentData, selectionFactory);
                }
                else
                {
                    var selectManyAttribute = GetSelectManyAttribute(property);
                    var selectionFactory = CreateSelectionFactoryInstance(selectManyAttribute.SelectionFactoryType);
                    return this._selectManyStrategy.Execute(property, contentData, selectionFactory);
                }
            }

            return stringValue;
        }

        private static ISelectionFactory CreateSelectionFactoryInstance(Type type)
        {
            return (ISelectionFactory) Activator.CreateInstance(type);
        }

        private static bool HasSelectAttribute(PropertyInfo property)
        {
            var selectOne = GetSelectOneAttribute(property);
            if (selectOne != null) return true;

            var selectMany = GetSelectManyAttribute(property);
            return selectMany != null;
        }

        private static SelectOneAttribute GetSelectOneAttribute(PropertyInfo propertyInfo)
        {
            var attribute = (SelectOneAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(SelectOneAttribute));
            return attribute;
        }

        private static SelectManyAttribute GetSelectManyAttribute(PropertyInfo propertyInfo)
        {
            var selectMany = (SelectManyAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(SelectManyAttribute));
            return selectMany;
        }
    }
}
