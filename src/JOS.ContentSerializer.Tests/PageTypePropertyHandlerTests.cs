using EPiServer.DataAbstraction;
using JOS.ContentSerializer.Internal.Default;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class PageTypePropertyHandlerTests
    {
        private readonly PageTypePropertyHandler _sut;

        public PageTypePropertyHandlerTests()
        {
            this._sut = new PageTypePropertyHandler(new ContentSerializerSettings());
        }

        [Fact]
        public void GivenNullPageType_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenPageType_WhenHandle_ThenReturnsCorrectValue()
        {
            var pageType = new PageType {Name = "anytype"};

            var result = this._sut.Handle(pageType, null, null);

            ((PageTypeModel)result).Name.ShouldBe(pageType.Name);
        }
    }
}
