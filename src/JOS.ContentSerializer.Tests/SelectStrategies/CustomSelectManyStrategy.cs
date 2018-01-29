using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Internal.Default;
using JOS.ContentSerializer.Tests.Pages;

namespace JOS.ContentSerializer.Tests.SelectStrategies
{
    public class CustomSelectManyStrategy : ISelectManyStrategy
    {
        private static readonly IReadOnlyDictionary<Type, Dictionary<string, bool>> CustomProperties =
            new Dictionary<Type, Dictionary<string, bool>>
            {
                {
                    typeof(StringPropertyHandlerPage), new Dictionary<string, bool>
                    {
                        {nameof(StringPropertyHandlerPage.SelectedOnlyMany), false},
                        {nameof(StringPropertyHandlerPage.SelectedOnlyValueOnlyMany), true}
                    }
                }
            };

        private readonly ISelectManyStrategy _defaultSelectManyStrategy;

        public CustomSelectManyStrategy()
        {
            this._defaultSelectManyStrategy = new DefaultSelectStrategy();
        }

        public object Execute(PropertyInfo property, IContentData contentData, ISelectionFactory selectionFactory)
        {
            var result = (IEnumerable<SelectOption>)this._defaultSelectManyStrategy.Execute(property, contentData, selectionFactory);
            var type = contentData.GetOriginalType();
            if (IsCustomContentType(type, property.Name))
            {
                var onlyValue = CustomProperties[type][property.Name];
                if (onlyValue)
                {
                    return result.Where(x => x.Selected).Select(x => x.Value);
                }

                return result.Where(x => x.Selected);
            }

            return result;
        }

        private static bool IsCustomContentType(Type type, string propertyName)
        {
            return CustomProperties.ContainsKey(type) && CustomProperties[type].ContainsKey(propertyName);
        }
    }
}
