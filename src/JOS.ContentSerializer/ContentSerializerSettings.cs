namespace JOS.ContentSerializer
{
    public class ContentSerializerSettings
    {
        public bool GlobalWrapContentAreaItems { get; set; } = true;

        public UrlSettings UrlSettings { get; set; } = new UrlSettings();

        public ContentReferenceSettings ContentReferenceSettings { get; set; } = new ContentReferenceSettings();
    }
}
