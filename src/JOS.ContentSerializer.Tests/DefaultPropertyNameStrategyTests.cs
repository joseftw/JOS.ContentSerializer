using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultPropertyNameStrategyTests
    {
        private readonly PropertyNameStrategy _sut;

        public DefaultPropertyNameStrategyTests()
        {
            this._sut = new PropertyNameStrategy();
        }

        [Fact]
        public void GivenNoContentSerializerNameAttribute_WhenGetPropertyName_ThenReturnsDeclaredName()
        {
            var page = new DefaultPropertyNameStrategyPage();

            var result = this._sut.GetPropertyName(page.GetType().GetProperty(nameof(DefaultPropertyNameStrategyPage.Heading)));

            result.ShouldBe("Heading");
        }

        [Fact]
        public void GivenContentSerializerNameAttribute_WhenGetPropertyName_ThenReturnsOverridenName()
        {
            var page = new DefaultPropertyNameStrategyPage();

            var result = this._sut.GetPropertyName(page.GetType().GetProperty(nameof(DefaultPropertyNameStrategyPage.Author)));

            result.ShouldBe("customAuthor");
        }
    }
}
