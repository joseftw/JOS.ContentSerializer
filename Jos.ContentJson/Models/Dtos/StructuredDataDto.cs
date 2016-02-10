using System.Reflection;

namespace Jos.ContentJson.Models.Dtos
{
    public class StructuredDataDto
    {
        public object Property { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}
