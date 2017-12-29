using JOS.ContentSerializer.Internal.Default;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.Pages
{
    public class StringArrayPropertyHandlerTests
    {
        private readonly StringArrayPropertyHandler _sut;

        public StringArrayPropertyHandlerTests()
        {
            this._sut = new StringArrayPropertyHandler();
        }

        [Fact]
        public void GivenNullStringArray_WhenGetValue_ThenReturnsEmptyArray()
        {
            var page = new StringArrayPropertyHandlerPage
            {
                Strings = null
            };

            var result = (string[])this._sut.Handle(
                null,
                page.GetType().GetProperty(nameof(StringArrayPropertyHandlerPage.Strings)),
                page);

            result.Length.ShouldBe(0);
        }

        [Fact]
        public void GivenPopulatedStringArray_WhenGetValue_ThenReturnsCorrectValues()
        {
            var expected = new [] {"Option 1", "Option 2", "Option 3"};
            var page = new StringArrayPropertyHandlerPage
            {
                Strings = expected
            };

            var result = this._sut.Handle(
                page.Strings,
                page.GetType().GetProperty(nameof(StringArrayPropertyHandlerPage.Strings)),
                page);

            result.ShouldBe(expected);
        }
    }
}
