using System;
using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DateTimePropertyHandlerTests
    {
        private readonly DateTimePropertyHandler _sut;
        public DateTimePropertyHandlerTests()
        {
            this._sut = new DateTimePropertyHandler(new ContentSerializerSettings());
        }

        [Fact]
        public void GivenDateTimeProperty_WhenHandle_ThenReturnsCorrectValue()
        {
            var expected = new DateTime(2000, 01, 01, 12, 00, 30);
            var page = new ValueTypePropertyHandlerPage
            {
                DateTime = expected
            };

            var result = (DateTime)this._sut.Handle(
                page.DateTime,
                page.GetType().GetProperty(nameof(ValueTypePropertyHandlerPage.DateTime)),
                page);

            result.ShouldBe(expected);
        }
    }
}
