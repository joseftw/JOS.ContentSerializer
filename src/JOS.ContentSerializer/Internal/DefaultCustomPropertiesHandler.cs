namespace JOS.ContentSerializer.Internal
{
    public class DefaultCustomPropertiesHandler : ICustomPropertiesHandler
    {
        public object GetValue(object propertyValue)
        {
            return propertyValue;
        }
    }
}
