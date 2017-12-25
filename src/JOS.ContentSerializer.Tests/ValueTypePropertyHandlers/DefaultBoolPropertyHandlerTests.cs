using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DefaultBoolPropertyHandlerTests
    {
        private readonly BoolPropertyHandler _sut;
        public DefaultBoolPropertyHandlerTests()
        {
            this._sut = new BoolPropertyHandler();
        }

        [Fact]
        public void GivenBoolProperty_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultValueTypePropertyHandlerPage
            {
                Bool = true
            };

            var result = (bool)this._sut.Handle(
                page.Bool,
                page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.Bool)),
                page);

            result.ShouldBeTrue();
        }
    }
}
