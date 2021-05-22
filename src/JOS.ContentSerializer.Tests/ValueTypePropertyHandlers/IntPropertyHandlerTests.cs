using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class IntPropertyHandlerTests
    {
        private readonly IntPropertyHandler _sut;

        public IntPropertyHandlerTests()
        {
            this._sut = new IntPropertyHandler(new ContentSerializerSettings());
        }

        [Fact]
        public void GivenIntProperty_WhenHandle_ThenReturnsCorrectValue()
        {
            var page = new ValueTypePropertyHandlerPage
            {
                Integer = 1000
            };

            var result = (int)this._sut.Handle(
                page.Integer,
                page.GetType().GetProperty(nameof(ValueTypePropertyHandlerPage.Integer)),
                page);

            result.ShouldBe(1000);
        }
    }
}
