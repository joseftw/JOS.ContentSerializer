namespace JOS.ContentSerializer.Internal
{
    public static class DefaultContentSerializerSettings
    {
        public static ContentSerializerSettings Instance => new ContentSerializerSettings
        {
            GlobalWrapContentAreaItems = true
        };
    }
}
