namespace JOS.ContentSerializer
{
    public class UrlSettings : IUrlSettings
    {
        public bool UseAbsoluteUrls { get; set; } = true;
        public bool FallbackToWildcard { get; set; } = true;
    }
}
