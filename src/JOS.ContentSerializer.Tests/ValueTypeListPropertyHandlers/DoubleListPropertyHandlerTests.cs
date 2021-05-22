using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypeListPropertyHandlers
{
    public class DoubleListPropertyHandlerTests
    {
        private readonly DoubleListPropertyHandler _sut;

        public DoubleListPropertyHandlerTests()
        {
            this._sut = new DoubleListPropertyHandler(new ContentSerializerSettings());
        }

        [Fact]
        public void GivenNullList_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenEmptyList_WhenHandle_ThenReturnsSameList()
        {
            var result = this._sut.Handle(Enumerable.Empty<double>(), null, null);

            ((IEnumerable<double>)result).ShouldBeEmpty();
        }

        [Fact]
        public void GivenPopulatedList_WhenHandle_ThenReturnsSameList()
        {
            var items = new List<double> { 1000, 20.50 };

            var result = this._sut.Handle(items, null, null);

            ((IEnumerable<double>)result).ShouldContain(1000);
            ((IEnumerable<double>)result).ShouldContain(20.50);
            ((IEnumerable<double>)result).Count().ShouldBe(2);
        }
    }
}
