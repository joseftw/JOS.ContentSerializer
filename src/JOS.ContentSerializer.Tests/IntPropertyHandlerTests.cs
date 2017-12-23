using System;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class IntPropertyHandlerTests
    {
        private readonly DefaultIntPropertyHandler _sut;

        public IntPropertyHandlerTests()
        {
            this._sut = new DefaultIntPropertyHandler();
        }

        [Fact]
        public void GivenIntProperty_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultValueTypePropertyHandlerPage
            {
                Integer = 1000
            };

            var result = (int)this._sut.Handle(
                page.Integer,
                page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.Integer)),
                page);

            result.ShouldBe(1000);
        }

        [Fact]
        public void GivenDoubleProperty_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultValueTypePropertyHandlerPage
            {
                Double = 10.50
            };

            var result = (double)this._sut.Handle(
                page.Integer,
                page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.Double)),
                page);

            result.ShouldBe(10.50);
        }

        //[Fact]
        //public void GivenBoolProperty_WhenGetValue_ThenReturnsCorrectValue()
        //{
        //    var page = new DefaultValueTypePropertyHandlerPage
        //    {
        //        Bool = true
        //    };

        //    var result = (bool)this._sut.Handle(
        //        page.Bool,
        //        page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.Bool)),
        //        page);

        //    result.ShouldBeTrue();
        //}

        //[Fact]
        //public void GivenDateTimeProperty_WhenGetValue_ThenReturnsCorrectValue()
        //{
        //    var expected = new DateTime(2000, 01, 01, 12, 00, 30);
        //    var page = new DefaultValueTypePropertyHandlerPage
        //    {
        //        DateTime = expected
        //    };

        //    var result = (DateTime)this._sut.Handle(
        //        page.DateTime,
        //        page.GetType().GetProperty(nameof(DefaultValueTypePropertyHandlerPage.DateTime)),
        //        page);

        //    result.ShouldBe(expected);
        //}
    }
}
