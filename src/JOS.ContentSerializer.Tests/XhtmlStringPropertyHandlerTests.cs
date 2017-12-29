using JOS.ContentSerializer.Internal.Default;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class XhtmlStringPropertyHandlerTests
    {
        private readonly XhtmlStringPropertyHandler _sut;

        public XhtmlStringPropertyHandlerTests()
        {
            this._sut = new XhtmlStringPropertyHandler();
        }

        [Fact]
        public void GivenNullXhtmlString_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null);

            result.ShouldBeNull();
        }
    }
}
