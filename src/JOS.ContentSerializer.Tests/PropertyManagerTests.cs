using System.Collections.Generic;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
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

        public PropertyManagerTests()
        {
            var contentLoader = Substitute.For<IContentLoader>();
            SetupContentLoader(contentLoader);
            this._sut = new PropertyManager(
                new DefaultValueTypePropertyHandler(),
                new DefaultPropertyNameStrategy(),
                new DefaultPropertyResolver(),
                new DefaultStringPropertyHandler(),
                new DefaultContentAreaPropertyHandler(contentLoader),
                new DefaultUrlPropertyHandler(
                    Substitute.For<IUrlHelper>(),
                    Substitute.For<ISiteDefinitionResolver>(),
                    Substitute.For<IRequestHostResolver>()),
                new DefaultStringArrayPropertyHandler(),
                new DefaultContentReferencePropertyHandler(Substitute.For<IUrlHelper>()),
                new DefaultPageTypePropertyHandler(),
                new DefaultContentReferenceListPropertyHandler(new DefaultContentReferencePropertyHandler(Substitute.For<IUrlHelper>())),
                new DefaultXhtmlStringPropertyHandler(),
                new DefaultLinkItemCollectionPropertyHandler(Substitute.For<IUrlHelper>())
            );
            this._page = new StandardPageBuilder().Build();
        }

        [Fact]
        public void GivenStringProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Heading)) && x.Value.Equals(_page.Heading));
        }

        [Fact]
        public void GivenIntProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Age)) && x.Value.Equals(_page.Age));
        }

        [Fact]
        public void GivenDoubleProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Degrees)) && x.Value.Equals(_page.Degrees));
        }

        [Fact]
        public void GivenBoolProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Private)) && x.Value.Equals(_page.Private));
        }

        [Fact]
        public void GivenDateTimeProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Starting)) && x.Value.Equals(_page.Starting));
        }

        [Fact]
        public void GivenContentAreaProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var contentArea = CreateContentArea();
            var page = new StandardPageBuilder().WithMainContentArea(contentArea).Build();

            var result = this._sut.GetStructuredData(page, new ContentSerializerSettings());

            result.ShouldContainKey(nameof(StandardPage.MainContentArea));
        }

        private static void SetupContentLoader(IContentLoader contentLoader)
        {
            contentLoader.Get<ContentData>(new ContentReference(1000))
                .Returns(new VideoBlock
                {
                    Name = "My name",
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
