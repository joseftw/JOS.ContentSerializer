using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class PropertyNameStrategyTests
    {
        private readonly PropertyNameStrategy _sut;

        public PropertyNameStrategyTests()
        {
            this._sut = new PropertyNameStrategy();
        }

        [Fact]
        public void GivenNoContentSerializerNameAttribute_WhenGetPropertyName_ThenReturnsDeclaredName()
        {
            var page = new PropertyNameStrategyPage();

            var result = this._sut.GetPropertyName(page.GetType().GetProperty(nameof(PropertyNameStrategyPage.Heading)));

            result.ShouldBe("Heading");
        }

        [Fact]
        public void GivenContentSerializerNameAttribute_WhenGetPropertyName_ThenReturnsOverridenName()
        {
            var page = new PropertyNameStrategyPage();

            var result = this._sut.GetPropertyName(page.GetType().GetProperty(nameof(PropertyNameStrategyPage.Author)));

            result.ShouldBe("customAuthor");
        }
    }
}
