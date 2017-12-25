using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Internal.ValueTypePropertyHandlers;
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
            var scanner = new DefaultPropertyHandlerScanner();
            scanner.Scan();
            this._sut = new PropertyManager(
                new DefaultPropertyNameStrategy(),
                new DefaultPropertyResolver(),
                new DefaultPropertyHandlerService(scanner)
            );
            this._page = new StandardPageBuilder().Build();
        }

        [Fact]
        public void GivenStringProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.GetInstance(typeof(IPropertyHandler<string>)).Returns(new StringPropertyHandler());
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Heading)) && x.Value.Equals(_page.Heading));
        }

        [Fact]
        public void GivenIntProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.GetInstance(typeof(IPropertyHandler<int>)).Returns(new IntPropertyHandler());
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Age)) && x.Value.Equals(_page.Age));
        }

        [Fact]
        public void GivenDoubleProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.GetInstance(typeof(IPropertyHandler<double>)).Returns(new DoublePropertyHandler());
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(_page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Degrees)) && x.Value.Equals(_page.Degrees));
        }

        [Fact]
        public void GivenBoolProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.GetInstance(typeof(IPropertyHandler<bool>)).Returns(new BoolPropertyHandler());
            ServiceLocator.SetLocator(serviceLocator);
            var page = new StandardPageBuilder().WithPrivate(true).Build();

            var result = this._sut.GetStructuredData(page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Private)) && x.Value.Equals(page.Private));
        }

        [Fact]
        public void GivenDateTimeProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.GetInstance(typeof(IPropertyHandler<DateTime>)).Returns(new DateTimePropertyHandler());
            ServiceLocator.SetLocator(serviceLocator);
            var expectedStartingDate = new DateTime(3000, 1,1);
            var page = new StandardPageBuilder().WithStarting(expectedStartingDate).Build();

            var result = this._sut.GetStructuredData(page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Starting)) && x.Value.Equals(page.Starting));
        }

        [Fact]
        public void GivenContentReferenceProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var contentReference = new ContentReference(2000);
            var page = new StandardPageBuilder().WithContentReference(contentReference).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            var urlHelper = Substitute.For<IUrlHelper>();
            var contentReferencePageUrl = "https://josefottosson.se/";
            urlHelper.ContentUrl(contentReference, Arg.Any<ContentReferenceSettings>()).Returns(contentReferencePageUrl);
            serviceLocator.GetInstance(typeof(IPropertyHandler<ContentReference>)).Returns(new ContentReferencePropertyHandler(urlHelper));
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.ContentReference)) && x.Value.Equals(contentReferencePageUrl));
        }

        [Fact]
        public void GivenPageReferenceProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var pageReference = new PageReference(3000);
            var page = new StandardPageBuilder().WithPageReference(pageReference).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            var urlHelper = Substitute.For<IUrlHelper>();
            var pageReferenceUrl = "https://josefottosson.se/";
            var contentReferencePropertyHandler = new ContentReferencePropertyHandler(urlHelper);
            urlHelper.ContentUrl(pageReference, Arg.Any<ContentReferenceSettings>()).Returns(pageReferenceUrl);
            serviceLocator.GetInstance(typeof(IPropertyHandler<PageReference>)).Returns(new PageReferencePropertyHandler(contentReferencePropertyHandler));
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, new ContentSerializerSettings());

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.PageReference)) && x.Value.Equals(pageReferenceUrl));
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
