using EPiServer.Core;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultContentReferencePropertyHandlerTests
    {
        private readonly ContentReferencePropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;
        private IContentSerializerSettings _contentSerializerSettings;

        public DefaultContentReferencePropertyHandlerTests()
        {
            this._contentSerializerSettings = Substitute.For<IContentSerializerSettings>();
            this._contentSerializerSettings.UrlSettings = new UrlSettings();
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new ContentReferencePropertyHandler(this._urlHelper, this._contentSerializerSettings);
        }

        [Fact]
        public void GivenContentReference_WhenGetValue_ThenReturnsAbsoluteUrlString()
        {
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            this._urlHelper.ContentUrl(contentReference, this._contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null);

            result.ShouldBe($"{baseUrl}{prettyPath}");
        }

        [Fact]
        public void GivenContentReference_WhenGetValueWithUseAbsoluteUrlsSetToFalse_ThenReturnsRelativeUrlString()
        {
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            this._contentSerializerSettings.UrlSettings.Returns(new UrlSettings { UseAbsoluteUrls = false });
            this._urlHelper.ContentUrl(Arg.Any<ContentReference>(), this._contentSerializerSettings.UrlSettings)
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null);

            result.ShouldBe(prettyPath);
        }
    }
}
