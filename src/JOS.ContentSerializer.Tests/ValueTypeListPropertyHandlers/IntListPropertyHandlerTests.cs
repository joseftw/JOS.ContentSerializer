using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypeListPropertyHandlers
{
    public class IntListPropertyHandlerTests
    {
        private readonly IntListPropertyHandler _sut;

        public IntListPropertyHandlerTests()
        {
            this._sut = new IntListPropertyHandler();
        }

        [Fact]
        public void GivenNullList_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null, null);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenEmptyList_WhenHandle_ThenReturnsSameList()
        {
            var result = this._sut.Handle(Enumerable.Empty<int>(), null, null, null);

            ((IEnumerable<int>)result).ShouldBeEmpty();
        }

        [Fact]
        public void GivenPopulatedList_WhenHandle_ThenReturnsSameList()
        {
            var items = new List<int> { 1000, 2000 };

            var result = this._sut.Handle(items, null, null, null);

            ((IEnumerable<int>)result).ShouldContain(1000);
            ((IEnumerable<int>)result).ShouldContain(2000);
            ((IEnumerable<int>)result).Count().ShouldBe(2);
        }
    }
}
