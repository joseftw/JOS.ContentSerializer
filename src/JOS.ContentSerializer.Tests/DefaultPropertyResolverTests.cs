using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultPropertyResolverTests
    {
        private readonly DefaultPropertyResolver _sut;

        public DefaultPropertyResolverTests()
        {
            this._sut = new DefaultPropertyResolver();
        }

        [Fact]
        public void GivenPage_WhenGetProperties_ThenReturnsCorrectProperties()
        {
            var page = new DefaultPropertyResolverPage();
            var expected = new List<string>
            {
                nameof(DefaultPropertyResolverPage.Heading),
                nameof(DefaultPropertyResolverPage.Description),
                nameof(DefaultPropertyResolverPage.Age),
                nameof(DefaultPropertyResolverPage.Starting),
                nameof(DefaultPropertyResolverPage.Private),
                nameof(DefaultPropertyResolverPage.Degrees),
                nameof(DefaultPropertyResolverPage.MainBody),
                nameof(DefaultPropertyResolverPage.MainContentArea),
                nameof(DefaultPropertyResolverPage.MainVideo),
                nameof(DefaultPropertyResolverPage.Include)
            };

            var result = this._sut.GetProperties(page).ToList();
            var includedPropertyNames = result.Select(x => x.Name);
            
            includedPropertyNames.ShouldBe(expected);
            result.ShouldNotContain(x => x.Name.Equals(nameof(DefaultPropertyResolverPage.Author)));
            result.ShouldNotContain(x => x.Name.Equals(nameof(DefaultPropertyResolverPage.Phone)));
        }
    }
}
