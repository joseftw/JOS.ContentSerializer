using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer
{
    /// <summary>
    /// Interface for creating property handlers with a handle method (Handle2) accepting the IContentSerializerSettings object
    /// as a parameter. Please use this interface instead of the IPropertyHandler one.
    /// For naming conventions, please see https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/general-naming-conventions
    /// </summary>
    /// <typeparam name="T">Type to be handled by this property handler.</typeparam>
    public interface IPropertyHandler2<in T>
    {
        object Handle2(T value, PropertyInfo property, IContentData contentData, IContentSerializerSettings contentSerializerSettings);
    }
}
