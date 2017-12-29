using System;
using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypeListPropertyHandlers
{
    public class DateTimeListPropertyHandlerTests
    {
        private readonly DateTimeListPropertyHandler _sut;

        public DateTimeListPropertyHandlerTests()
        {
            this._sut = new DateTimeListPropertyHandler();
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
            var result = this._sut.Handle(Enumerable.Empty<DateTime>(), null, null);

            ((IEnumerable<DateTime>)result).ShouldBeEmpty();
        }

        [Fact]
        public void GivenPopulatedList_WhenHandle_ThenReturnsSameList()
        {
            var year2000 = new DateTime(2000, 1, 20);
            var year3000 = new DateTime(3000, 1, 20);
            var items = new List<DateTime> { year2000, year3000};

            var result = this._sut.Handle(items, null, null);

            ((IEnumerable<DateTime>)result).ShouldContain(year2000);
            ((IEnumerable<DateTime>)result).ShouldContain(year3000);
            ((IEnumerable<DateTime>)result).Count().ShouldBe(2);
        }
    }
}
