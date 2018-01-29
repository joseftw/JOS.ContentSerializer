using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Internal.Default;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests.ValueTypePropertyHandlers
{
    public class StringPropertyHandlerTests
    {
        private readonly StringPropertyHandler _sut;

        public StringPropertyHandlerTests()
        {
            var selectStrategy = new DefaultSelectStrategy();
            this._sut = new StringPropertyHandler(selectStrategy, selectStrategy);
        }

        [Theory]
        [InlineData("This is the heading of the page")]
        [InlineData("")]
        [InlineData(null)]
        public void GivenStringProperty_WhenHandle_ThenReturnsCorrectValue(string heading)
        {
            var page = new StringPropertyHandlerPage
            {
                Heading = heading
            };

            var result = this._sut.Handle(page.Heading,
                page.GetType().GetProperty(nameof(StringPropertyHandlerPage.Heading)),
                page);

            ((string)result).ShouldBe(heading);
        }

        [Fact]
        public void GivenStringPropertyWithSelectOneAttribute_WhenHandle_ThenReturnsCorrectValue()
        {
            var page = new StringPropertyHandlerPage
            {
                SelectOne = "option3"
            };

            var result = (List<SelectOption>)this._sut.Handle(page.SelectOne,
                page.GetType().GetProperty(nameof(StringPropertyHandlerPage.SelectOne)),
                page);

            result.ShouldContain(x => x.Selected && x.Value.Equals("option3") && x.Text.Equals("Option 3"));
            result.Count(x => x.Selected).ShouldBe(1);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNullOrEmptyStringWithSelectOneAttribute_WhenGet_ThenReturnsCorrectValue(string selectOneValue)
        {
            var page = new StringPropertyHandlerPage
            {
                SelectOne = selectOneValue
            };

            var result = (IEnumerable<SelectOption>)this._sut.Handle(page.SelectOne,
                page.GetType().GetProperty(nameof(StringPropertyHandlerPage.SelectOne)),
                page);

            result.Count(x => x.Selected).ShouldBe(0);
        }

        [Fact]
        public void GivenStringPropertyWithSelectManyAttribute_WhenHandle_ThenReturnsCorrectValue()
        {
            var page = new StringPropertyHandlerPage
            {
                SelectMany = "option3,option4,option5"
            };

            var result = (List<SelectOption>)this._sut.Handle(page.SelectMany,
                page.GetType().GetProperty(nameof(StringPropertyHandlerPage.SelectMany)),
                page);

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
            var page = new StringPropertyHandlerPage
            {
                SelectMany = selectManyValue
            };

            var result = (IEnumerable<SelectOption>)this._sut.Handle(page.SelectMany,
                page.GetType().GetProperty(nameof(StringPropertyHandlerPage.SelectOne)),
                page);

            result.Count(x => x.Selected).ShouldBe(0);
        }
    }
}
