using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DoublePropertyHandlerTests
    {
        private readonly DoublePropertyHandler _sut;
        public DoublePropertyHandlerTests()
        {
            this._sut = new DoublePropertyHandler();
        }

        [Fact]
        public void GivenDoubleProperty_WhenHandle_ThenReturnsCorrectValue()
        {
            var page = new ValueTypePropertyHandlerPage
            {
                Double = 10.50
            };

            var result = (double)this._sut.Handle(
                page.Double,
                page.GetType().GetProperty(nameof(ValueTypePropertyHandlerPage.Double)),
                page);

            result.ShouldBe(10.50);
        }
    }
}
