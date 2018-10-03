using EPiServer.Core;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class ContentReferencePropertyHandlerTests
    {
        private readonly ContentReferencePropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;
        private IContentSerializerSettings _contentSerializerSettings;

        public ContentReferencePropertyHandlerTests()
        {
            this._contentSerializerSettings = Substitute.For<IContentSerializerSettings>();
            this._contentSerializerSettings.UrlSettings = new UrlSettings();
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new ContentReferencePropertyHandler(this._urlHelper);
        }

        [Fact]
        public void GivenNullContentReference_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null, this._contentSerializerSettings);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenEmptyContentReference_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(ContentReference.EmptyReference, null, null, this._contentSerializerSettings);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenContentReference_WhenHandle_ThenReturnsAbsoluteUrlString()
        {
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            this._urlHelper.ContentUrl(contentReference, this._contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null, this._contentSerializerSettings);

            result.ShouldBe($"{baseUrl}{prettyPath}");
        }

        [Fact]
        public void GivenContentReference_WhenHandleWithUseAbsoluteUrlsSetToFalse_ThenReturnsRelativeUrlString()
        {
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            this._contentSerializerSettings.UrlSettings.Returns(new UrlSettings { UseAbsoluteUrls = false });
            this._urlHelper.ContentUrl(Arg.Any<ContentReference>(), this._contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null, this._contentSerializerSettings);

            result.ShouldBe(prettyPath);
        }
    }
}
