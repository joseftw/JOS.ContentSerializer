using JOS.ContentSerializer.Internal;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.Pages
{
    public class DefaultStringArrayPropertyHandlerTests
    {
        private readonly DefaultStringArrayPropertyHandler _sut;

        public DefaultStringArrayPropertyHandlerTests()
        {
            this._sut = new DefaultStringArrayPropertyHandler();
        }

        [Fact]
        public void GivenNullStringArray_WhenGetValue_ThenReturnsEmptyArray()
        {
            var page = new DefaultStringArrayPropertyHandlerPage
            {
                Strings = null
            };

            var result = this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringArrayPropertyHandlerPage.Strings)));

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

            var result = this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringArrayPropertyHandlerPage.Strings)));

            result.ShouldBe(expected);
        }
    }
}
