using JOS.ContentSerializer.Internal.Default;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class BoolPropertyHandlerTests
    {
        private readonly BoolPropertyHandler _sut;
        public BoolPropertyHandlerTests()
        {
            this._sut = new BoolPropertyHandler();
        }

        [Fact]
        public void GivenBoolProperty_WhenHandle_ThenReturnsCorrectValue()
        {
            var page = new ValueTypePropertyHandlerPage
            {
                Bool = true
            };

            var result = (bool)this._sut.Handle(
                page.Bool,
                page.GetType().GetProperty(nameof(ValueTypePropertyHandlerPage.Bool)),
                page);

            result.ShouldBeTrue();
        }
    }
}
