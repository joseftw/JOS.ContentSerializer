using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypeListPropertyHandlers
{
    public class StringListPropertyHandlerTests
    {
        private readonly StringListPropertyHandler _sut;

        public StringListPropertyHandlerTests()
        {
            this._sut = new StringListPropertyHandler();
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
            var result = this._sut.Handle(Enumerable.Empty<string>(), null, null, null);

            ((IEnumerable<string>)result).ShouldBeEmpty();
        }

        [Fact]
        public void GivenPopulatedList_WhenHandle_ThenReturnsSameList()
        {
            var items = new List<string>{"any", "value"};

            var result = this._sut.Handle(items, null, null, null);

            ((IEnumerable<string>)result).ShouldContain("any");
            ((IEnumerable<string>)result).ShouldContain("value");
            ((IEnumerable<string>)result).Count().ShouldBe(2);
        }

        [Fact]
        public void GivenStringArray_WhenHandle_ThenReturnsIEnumerableString()
        {
            var items = new[] { "any", "value" };

            var result = this._sut.Handle(items, null, null, null);

            ((IEnumerable<string>)result).ShouldContain("any");
            ((IEnumerable<string>)result).ShouldContain("value");
            ((IEnumerable<string>)result).Count().ShouldBe(2);
        }
    }
}
