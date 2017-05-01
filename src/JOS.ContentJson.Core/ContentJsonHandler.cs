namespace JOS.ContentJson.Core
{
    public class ContentJsonHandler : IContentJsonHandler
    {
        public string Execute(object contentData)
        {
            return "HEJ";
        }

        public string Execute(object contentData, bool? wrapItems)
        {
            return "TJA";
        }
    }
}
