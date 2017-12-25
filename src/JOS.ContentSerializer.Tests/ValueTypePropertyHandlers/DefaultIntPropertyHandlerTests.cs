using JOS.ContentSerializer.Internal.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DefaultIntPropertyHandlerTests
    {
        private readonly DefaultIntPropertyHandler _sut;

        public DefaultIntPropertyHandlerTests()
        {
            this._sut = new DefaultIntPropertyHandler();
        }

        [Fact]
        public void GivenIntProperty_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultValueTypePropertyHandlerPage
            {
                Integer = 1000
            };

            var result = (int)this._sut.Handle(
                page.Integer,
                page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.Integer)),
                page);

            result.ShouldBe(1000);
        }
    }
}
