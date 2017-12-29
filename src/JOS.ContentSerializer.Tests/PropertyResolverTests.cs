using System.Collections.Generic;
using System.Linq;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class PropertyResolverTests
    {
        private readonly PropertyResolver _sut;

        public PropertyResolverTests()
        {
            this._sut = new PropertyResolver();
        }

        [Fact]
        public void GivenPage_WhenGetProperties_ThenReturnsCorrectProperties()
        {
            var page = new PropertyResolverPage();
            var expected = new List<string>
            {
                nameof(PropertyResolverPage.Heading),
                nameof(PropertyResolverPage.Description),
                nameof(PropertyResolverPage.Age),
                nameof(PropertyResolverPage.Starting),
                nameof(PropertyResolverPage.Private),
                nameof(PropertyResolverPage.Degrees),
                nameof(PropertyResolverPage.MainBody),
                nameof(PropertyResolverPage.MainContentArea),
                nameof(PropertyResolverPage.MainVideo),
                nameof(PropertyResolverPage.Include)
            };

            var result = this._sut.GetProperties(page).ToList();
            var includedPropertyNames = result.Select(x => x.Name);
            
            includedPropertyNames.ShouldBe(expected);
            result.ShouldNotContain(x => x.Name.Equals(nameof(PropertyResolverPage.Author)));
            result.ShouldNotContain(x => x.Name.Equals(nameof(PropertyResolverPage.Phone)));
        }
    }
}
