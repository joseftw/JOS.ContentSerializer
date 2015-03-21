using ContentJson.Helpers;
using EPiServer.Core;
using Newtonsoft.Json;

namespace ContentJson.Extensions
{
    public static class ContentDataExtensions
    {
        private static readonly ContentJsonHelper ContentJsonHelper = new ContentJsonHelper();

        public static string ToJson(this ContentData contentData)
        {
            var propertiesDict = ContentJsonHelper.GetStructuredDictionary(contentData);

            return JsonConvert.SerializeObject(propertiesDict);
        }
    }
}
