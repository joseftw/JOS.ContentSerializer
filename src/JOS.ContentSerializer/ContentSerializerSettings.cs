namespace JOS.ContentSerializer
{
    public class ContentSerializerSettings : IContentSerializerSettings
    {
        public bool WrapContentAreaItems { get; set; } = true;
        public IUrlSettings UrlSettings { get; set; } = new UrlSettings();
        public string BlockTypePropertyName { get; set; } = "__type__";
        public bool IgnoreEmptyValues { get; set; } = false;
    }
}
