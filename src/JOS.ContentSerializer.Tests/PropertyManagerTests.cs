using System.Collections.Generic;
using System.Reflection;
using EPiServer;
using EPiServer.Core;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class PropertyManagerTests
    {
        private readonly PropertyManager _sut;
        private readonly StandardPage _page;
        private readonly IEnumerable<PropertyInfo> _properties;

        public PropertyManagerTests()
        {
            var contentLoader = Substitute.For<IContentLoader>();
            SetupContentLoader(contentLoader);
            this._sut = new PropertyManager(
                new DefaultPropertyNameStrategy(),
                new DefaultStringPropertyHandler(),
                new DefaultContentAreaPropertyHandler(
                    contentLoader,
                    new DefaultContentDataPropertyHandler())
            );
            this._page = new StandardPageBuilder().Build();
            this._properties = new DefaultPropertyResolver().GetProperties(this._page);
        }

        [Fact]
        public void GivenStringProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, _properties, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Heading)) && x.Value.Equals(_page.Heading));
        }

        [Fact]
        public void GivenIntProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, _properties, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Age)) && x.Value.Equals(_page.Age));
        }

        [Fact]
        public void GivenDoubleProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, _properties, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Degrees)) && x.Value.Equals(_page.Degrees));
        }

        [Fact]
        public void GivenBoolProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, _properties, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Private)) && x.Value.Equals(_page.Private));
        }

        [Fact]
        public void GivenDateTimeProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, _properties, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Starting)) && x.Value.Equals(_page.Starting));
        }

        [Fact]
        public void GivenContentAreaProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var contentArea = CreateContentArea();
            var page = new StandardPageBuilder().WithMainContentArea(contentArea).Build();
            var result = this._sut.GetStructuredData(page, _properties, new ContentSerializerSettings());
        }

        private void SetupContentLoader(IContentLoader contentLoader)
        {
            contentLoader.Get<ContentData>(new ContentReference(1000))
                .Returns(new VideoBlock
                {
                    Url = new Url("https://josef.guru")
                });
        }

        private static ContentArea CreateContentArea()
        {
            var contentArea = Substitute.For<ContentArea>();
            var items = new List<ContentAreaItem>
            {
                new ContentAreaItem
                {
                    ContentLink = new ContentReference(1000)
                }
            };
            contentArea.Count.Returns(items.Count);
            contentArea.Items.Returns(items);
            
            return contentArea;
        }
    }
}
