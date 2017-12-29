using EPiServer;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultUrlPropertyHandlerTests
    {
        private readonly UrlPropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;
        private IContentSerializerSettings _contentSerializerSettings;
        
        public DefaultUrlPropertyHandlerTests()
        {
            this._contentSerializerSettings = Substitute.For<IContentSerializerSettings>();
            this._contentSerializerSettings.UrlSettings = new UrlSettings();
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new UrlPropertyHandler(this._urlHelper, this._contentSerializerSettings);
        }

        [Fact]
        public void GivenMailToUrl_WhenGetValue_ThenReturnsCorrectValue()
        {
            var value = "mailto:mail@example.com";
            var url = new Url(value);

            var result = this._sut.Handle(url, null, null);

            result.ShouldBe(value);
        }

        [Fact]
        public void GivenExternalLink_WhenGetValue_ThenReturnsAbsoulteUrlWithQuery()
        {
            var value = "https://josef.guru/example/page?anyQueryString=true&anyOtherQuery";
            var url = new Url(value);

            var result = this._sut.Handle(url, null, null);

            result.ShouldBe(value);
        }

        [Fact]
        public void GivenExternalLink_WhenGetValueWithAbsoluteUrlSetToFalse_ThenReturnsRelativeUrlWithQuery()
        {
            this._contentSerializerSettings.UrlSettings.Returns(new UrlSettings {UseAbsoluteUrls = false});
            var value = "https://josef.guru/example/page?anyQueryString=true&anyOtherQuery";
            var url = new Url(value);

            var result = this._sut.Handle(url, null, null);

            result.ShouldBe(url.PathAndQuery);
        }

        [Fact]
        public void GivenEpiserverPage_WhenGetValue_ThenReturnsRewrittenPrettyAbsoluteUrl()
        {
            var siteUrl = "https://example.com";
            var prettyPath = "/rewritten/pretty-url/";
            var value = "/link/d40d0056ede847d5a2f3b4a02778d15b.aspx";
            var url = new Url(value);
            this._urlHelper.ContentUrl(Arg.Any<Url>(), this._contentSerializerSettings.UrlSettings).Returns($"{siteUrl}{prettyPath}");
         
            var result = this._sut.Handle(url, null, null);

            result.ShouldBe($"{siteUrl}{prettyPath}");
        }

        [Fact]
        public void GivenEpiserverPage_WhenGetValueWithAbsoluteUrlSetToFalse_ThenReturnsRewrittenPrettyRelativeUrl()
        {
            var prettyPath = "/rewritten/pretty-url/";
            var value = "/link/d40d0056ede847d5a2f3b4a02778d15b.aspx";
            var url = new Url(value);
            this._contentSerializerSettings.UrlSettings.Returns(new UrlSettings { UseAbsoluteUrls = false });
            this._urlHelper.ContentUrl(Arg.Any<Url>(), this._contentSerializerSettings.UrlSettings).Returns(prettyPath);

            var result = this._sut.Handle(url, null, null);

            result.ShouldBe(prettyPath);
        }
    }
}
