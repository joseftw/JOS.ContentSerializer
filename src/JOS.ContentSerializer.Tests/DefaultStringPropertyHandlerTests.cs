using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultStringPropertyHandlerTests
    {
        private readonly DefaultStringPropertyHandler _sut;

        public DefaultStringPropertyHandlerTests()
        {
            this._sut = new DefaultStringPropertyHandler();
        }

        [Theory]
        [InlineData("This is the heading of the page")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenStringProperty_WhenGetValue_ThenReturnsCorrectValue(string heading)
        {
            var page = new DefaultStringPropertyHandlerPage
            {
                Heading = heading
            };

            var result = this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringPropertyHandlerPage.Heading)));
  
            ((string)result).ShouldBe(heading);
        }

        [Fact]
        public void GivenStringPropertyWithSelectOneAttribute_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultStringPropertyHandlerPage
            {
                SelectOne = "option3"
            };

            var result = (List<SelectOptionDto>)this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringPropertyHandlerPage.SelectOne)));

            result.ShouldContain(x => x.Selected && x.Value.Equals("option3") && x.Text.Equals("Option 3"));
            result.Count(x => x.Selected).ShouldBe(1);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNullOrEmptyStringWithSelectOneAttribute_WhenGet_ThenReturnsCorrectValue(string selectOneValue)
        {
            var page = new DefaultStringPropertyHandlerPage
            {
                SelectOne = selectOneValue
            };

            var result = (List<SelectOptionDto>)this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringPropertyHandlerPage.SelectOne)));

            result.Count(x => x.Selected).ShouldBe(0);
        }

        [Fact]
        public void GivenStringPropertyWithSelectManyAttribute_WhenGetValue_ThenReturnsCorrectValue()
        {
            var page = new DefaultStringPropertyHandlerPage
            {
                SelectMany = "option3,option4,option5"
            };

            var result = (List<SelectOptionDto>)this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringPropertyHandlerPage.SelectMany)));

            result.ShouldContain(x => x.Selected && x.Value.Equals("option3") && x.Text.Equals("Option 3"));
            result.ShouldContain(x => x.Selected && x.Value.Equals("option4") && x.Text.Equals("Option 4"));
            result.ShouldContain(x => x.Selected && x.Value.Equals("option5") && x.Text.Equals("Option 5"));
            result.Count(x => x.Selected).ShouldBe(3);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNullOrEmptyStringWithSelectManyAttribute_WhenGet_ThenReturnsCorrectValue(string selectManyValue)
        {
            var page = new DefaultStringPropertyHandlerPage
            {
                SelectMany = selectManyValue
            };

            var result = (List<SelectOptionDto>)this._sut.GetValue(page,
                page.GetType().GetProperty(nameof(DefaultStringPropertyHandlerPage.SelectOne)));

            result.Count(x => x.Selected).ShouldBe(0);
        }
    }
}
