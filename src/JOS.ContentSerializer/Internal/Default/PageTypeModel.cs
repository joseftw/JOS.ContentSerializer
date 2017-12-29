namespace JOS.ContentSerializer.Internal.Default
{
    public class PageTypeModel
    {
        public PageTypeModel(string name, int contentTypeId)
        {
            Name = name;
            ContentTypeId = contentTypeId;
        }

        public string Name { get; }
        public int ContentTypeId { get; }
    }
}
