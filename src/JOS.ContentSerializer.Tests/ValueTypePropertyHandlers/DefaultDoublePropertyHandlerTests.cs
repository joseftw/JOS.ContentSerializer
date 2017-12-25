using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DefaultDoublePropertyHandlerTests
    {
        private readonly DoublePropertyHandler _sut;
        public DefaultDoublePropertyHandlerTests()
        {
            this._sut = new DoublePropertyHandler();
        }

        [Fact]
        public void GivenDoubleProperty_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultValueTypePropertyHandlerPage
            {
                Double = 10.50
            };

            var result = (double)this._sut.Handle(
                page.Double,
                page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.Double)),
                page);

            result.ShouldBe(10.50);
        }
    }
}
