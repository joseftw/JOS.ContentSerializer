using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;
using JOS.ContentSerializer.Internal;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultContentReferencePropertyHandlerTests
    {
        private readonly DefaultContentReferencePropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;

        public DefaultContentReferencePropertyHandlerTests()
        {
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new DefaultContentReferencePropertyHandler(this._urlHelper);
        }

        [Fact]
        public void GivenContentReference_WhenGetValue_ThenReturnsAbsoluteUrlString()
        {
            var host = "example.com";
            var scheme = "https://";
            var baseUrl = $"{scheme}{host}";
            var prettyPath = "/any-path/to/page/?anyQueryParam=value&anotherQuery";
            var contentReference = new ContentReference(1000);
            this._urlHelper.ContentUrl(Arg.Any<ContentReference>(), Arg.Any<ContentReferenceSettings>())
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
            this._urlHelper.ContentUrl(Arg.Any<ContentReference>(), Arg.Any<ContentReferenceSettings>())
                .Returns($"{baseUrl}{prettyPath}");

            var result = this._sut.Handle(contentReference, null, null);

            result.ShouldBe(prettyPath);
        }
    }
}
