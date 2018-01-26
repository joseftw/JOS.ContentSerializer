using System;
using System.Diagnostics;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace JOS.ContentSerializer
{
    public static class IContentDataExtensions
    {
        public static string Serialize(this IContentData contentData)
        {
            var contentSerializer = ServiceLocator.Current.GetInstance<IContentSerializer>();
            return contentSerializer.Serialize(contentData);
        }

        public static string Serialize(this IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            var contentSerializer = ServiceLocator.Current.GetInstance<IContentSerializer>();
            return contentSerializer.Serialize(contentData, contentSerializerSettings);
        }

        public static string ToJson(this IContentData contentData)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var contentSerializer = GetContentJsonSerializer();
            var result = contentSerializer.Serialize(contentData);
            stopwatch.Stop();
            Trace.WriteLine($".ToJson took {stopwatch.ElapsedMilliseconds}ms");
            return result;
        }

        public static string ToJson(this IContentData contentData, IContentSerializerSettings contentSerializerSettings)
        {
            var contentSerializer = GetContentJsonSerializer();
            return contentSerializer.Serialize(contentData, contentSerializerSettings);
        }

        private static IContentJsonSerializer GetContentJsonSerializer()
        {
            var contentSerializer = ServiceLocator.Current.GetInstance<IContentJsonSerializer>();
            return contentSerializer ?? throw new Exception($"No implementation of {nameof(IContentJsonSerializer)} could be found");
        }
    }
}
