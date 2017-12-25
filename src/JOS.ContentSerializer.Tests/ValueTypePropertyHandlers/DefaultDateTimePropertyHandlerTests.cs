using System;
using JOS.ContentSerializer.Internal.ValueTypePropertyHandlers;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class DefaultDateTimePropertyHandlerTests
    {
        private readonly DateTimePropertyHandler _sut;
        public DefaultDateTimePropertyHandlerTests()
        {
            this._sut = new DateTimePropertyHandler();
        }

        [Fact]
        public void GivenDateTimeProperty_WhenGetValue_ThenReturnsCorrectValue()
        {
            var expected = new DateTime(2000, 01, 01, 12, 00, 30);
            var page = new DefaultValueTypePropertyHandlerPage
            {
                DateTime = expected
            };

            var result = (DateTime)this._sut.Handle(
                page.DateTime,
                page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.DateTime)),
                page);

            result.ShouldBe(expected);
        }
    }
}
