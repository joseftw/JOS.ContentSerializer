using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Internal.Default;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.Pages
{
    public class DefaultStringArrayPropertyHandlerTests
    {
        private readonly StringArrayPropertyHandler _sut;

        public DefaultStringArrayPropertyHandlerTests()
        {
            this._sut = new StringArrayPropertyHandler();
        }

        [Fact]
        public void GivenNullStringArray_WhenGetValue_ThenReturnsEmptyArray()
        {
            var page = new DefaultStringArrayPropertyHandlerPage
            {
                Strings = null
            };

            var result = (string[])this._sut.Handle(
                null,
                page.GetType().GetProperty(nameof(DefaultStringArrayPropertyHandlerPage.Strings)),
                page);

            result.Length.ShouldBe(0);
        }

        [Fact]
        public void GivenPopulatedStringArray_WhenGetValue_ThenReturnsCorrectValues()
        {
            var expected = new [] {"Option 1", "Option 2", "Option 3"};
            var page = new DefaultStringArrayPropertyHandlerPage
            {
                Strings = expected
            };

            var result = this._sut.Handle(
                null,
                page.GetType().GetProperty(nameof(DefaultStringArrayPropertyHandlerPage.Strings)),
                page);

            result.ShouldBe(expected);
        }
    }
}
