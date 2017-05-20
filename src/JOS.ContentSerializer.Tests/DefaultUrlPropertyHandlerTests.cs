using System;
using EPiServer;
using EPiServer.Web;
using JOS.ContentSerializer.Internal;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultUrlPropertyHandlerTests
    {
        private readonly DefaultUrlPropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;
        private readonly IRequestHostResolver _requestHostResolver;

        public DefaultUrlPropertyHandlerTests()
        {
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._siteDefinitionResolver = Substitute.For<ISiteDefinitionResolver>();
            this._requestHostResolver = Substitute.For<IRequestHostResolver>();
            this._sut = new DefaultUrlPropertyHandler(this._urlHelper, this._siteDefinitionResolver, this._requestHostResolver);
        }

        [Fact]
        public void GivenMailToUrl_WhenGetValue_ThenReturnsCorrectValue()
        {
            var value = "mailto:mail@example.com";
            var url = new Url(value);

            var result = this._sut.GetValue(url, true);

            result.ShouldBe(value);
        }

        [Fact]
        public void GivenExternalLink_WhenGetValue_ThenReturnsAbsoulteUrlWithQuery()
        {
            var value = "https://josef.guru/example/page?anyQueryString=true&anyOtherQuery";
            var url = new Url(value);

            var result = this._sut.GetValue(url, true);

            result.ShouldBe(value);
        }

        [Fact]
        public void GivenExternalLink_WhenGetValueWithAbsoluteUrlSetToFalse_ThenReturnsRelativeUrlWithQuery()
        {
            var value = "https://josef.guru/example/page?anyQueryString=true&anyOtherQuery";
            var url = new Url(value);

            var result = this._sut.GetValue(url, false);

            result.ShouldBe(url.PathAndQuery);
        }

        [Fact]
        public void GivenEpiserverPage_WhenGetValue_ThenReturnsRewrittenPrettyAbsoluteUrl()
        {
            var siteUrl = "https://example.com";
            var prettyPath = "/rewritten/pretty-url/";
            var value = "/link/d40d0056ede847d5a2f3b4a02778d15b.aspx";
            var url = new Url(value);
            this._urlHelper.ContentUrl(Arg.Any<Url>()).Returns(prettyPath);
            this._requestHostResolver.HostName.Returns("example.com");
            this._siteDefinitionResolver.GetByHostname(Arg.Any<string>(), true).Returns(new SiteDefinition
            {
                SiteUrl = new Uri(siteUrl)
            });

            var result = this._sut.GetValue(url, true);

            result.ShouldBe($"{siteUrl}{prettyPath}");
        }

        [Fact]
        public void GivenEpiserverPage_WhenGetValueWithAbsoluteUrlSetToFalse_ThenReturnsRewrittenPrettyRelativeUrl()
        {
            var prettyPath = "/rewritten/pretty-url/";
            var value = "/link/d40d0056ede847d5a2f3b4a02778d15b.aspx";
            var url = new Url(value);
            this._urlHelper.ContentUrl(Arg.Any<Url>()).Returns(prettyPath);

            var result = this._sut.GetValue(url, false);

            result.ShouldBe(prettyPath);
        }
    }
}
