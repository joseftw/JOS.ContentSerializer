using System.Collections;
using System.Collections.Generic;

namespace Jos.ContentJson.Extensions
{
    public static class GenericListExtensions
    {
        public static IList GetStructuredData(this IList list)
        {
            if (list == null) return null;

            var structuredDataList = new List<object>();
            foreach (var item in list)
            {
                var propertyDict = new Dictionary<string, object>();
                foreach (var property in item.GetType().GetProperties())
                {
                    var structuredData = property.GetProcessedData(item);

                    if (structuredData == null) continue;

                    var jsonKey = property.GetJsonKey();
                    propertyDict.Add(jsonKey, structuredData);
                }

                structuredDataList.Add(propertyDict);
            }

            return structuredDataList;
        }
    }
}
