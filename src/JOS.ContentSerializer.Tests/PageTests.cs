using JOS.ContentSerializer.Internal;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class PageTests
    {
        private readonly ContentSerializer _sut;
        public PageTests()
        {
            this._sut = new ContentSerializer(
                new DefaultJsonContentSerializer(
                    new DefaultPropertyResolver(),
                    new PropertyManager(new DefaultPropertyNameStrategy())
            ));
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
