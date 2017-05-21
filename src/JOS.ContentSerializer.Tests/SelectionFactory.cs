using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;

namespace JOS.ContentSerializer.Tests
{
    public class SelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new List<ISelectItem>
            {
                new SelectItem {Text = "Option 1", Value = "option1"},
                new SelectItem {Text = "Option 2", Value = "option2"},
                new SelectItem {Text = "Option 3", Value = "option3"},
                new SelectItem {Text = "Option 4", Value = "option4"},
                new SelectItem {Text = "Option 5", Value = "option5"},
                new SelectItem {Text = "Option 6", Value = "option6"},
                new SelectItem {Text = "Option 7", Value = "option7"},
                new SelectItem {Text = "Option 8", Value = "option8"},
                new SelectItem {Text = "Option 9", Value = "option9"},
                new SelectItem {Text = "Option 10", Value = "option10"},
            };
        }
    }
}
