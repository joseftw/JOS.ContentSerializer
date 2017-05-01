namespace JOS.ContentJson.Core
{
    public interface IContentJsonHandler
    {
        string Execute(object contentData);
        string Execute(object contentData, bool? wrapItems);
    }
}
