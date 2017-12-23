using System.Reflection;
using EPiServer.Core;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultDoublePropertyHandler : IPropertyHandler<double>
    {
        public object Handle(double value, PropertyInfo property, IContentData contentData)
        {
            return value;
        }
    }
}
