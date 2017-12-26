namespace JOS.ContentSerializer
{
    public interface IContentSerializerSettings
    {
        bool WrapContentAreaItems { get; set; }
        IUrlSettings UrlSettings { get; set; }
        string BlockTypePropertyName { get; set; }
    }
}
