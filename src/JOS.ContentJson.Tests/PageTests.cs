using JOS.ContentJson.Core;
using Shouldly;
using Xunit;

namespace JOS.ContentJson.Tests
{
    public class PageTests
    {
        private readonly ContentJson _sut;
        public PageTests()
        {
            this._sut = new ContentJson(new ContentJsonHandler());
        }

        [Fact]
        public void Hej()
        {
            var page = new StandardPageBuilder().Build();

            var result = this._sut.ToJson(page);

            result.ShouldBe("HEJ");
        }
    }
}
