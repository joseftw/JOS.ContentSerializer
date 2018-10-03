using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Internal.Default;
using JOS.ContentSerializer.Internal.Default.ValueListPropertyHandlers;
using JOS.ContentSerializer.Internal.Default.ValueTypePropertyHandlers;
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
        private readonly IContentSerializerSettings _contentSerializerSettings;
        private readonly IContentLoader _contentLoader;
        public PropertyManagerTests()
        {
            this._contentSerializerSettings = Substitute.For<IContentSerializerSettings>();
            this._contentSerializerSettings.UrlSettings = new UrlSettings();
            this._contentLoader = Substitute.For<IContentLoader>();
            SetupContentLoader(this._contentLoader);
            this._sut = new PropertyManager(
                new PropertyNameStrategy(),
                new PropertyResolver(),
                new PropertyHandlerService(),
                _contentSerializerSettings
            );
            this._page = new StandardPageBuilder().Build();
        }

        [Fact]
        public void GivenStringProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<string>), out var _).Returns(x =>
            {
                var selectStrategy = new DefaultSelectStrategy();
                x[1] = new StringPropertyHandler(selectStrategy, selectStrategy);
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(_page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Heading)) && x.Value.Equals(_page.Heading));
        }

        [Fact]
        public void GivenIntProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<int>), out var _).Returns(x =>
            {
                x[1] = new IntPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(_page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Age)) && x.Value.Equals(_page.Age));
        }

        [Fact]
        public void GivenDoubleProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<double>), out var _).Returns(x =>
            {
                x[1] = new DoublePropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(_page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Degrees)) && x.Value.Equals(_page.Degrees));
        }

        [Fact]
        public void GivenBoolProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<bool>), out var _).Returns(x =>
            {
                x[1] = new BoolPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);
            var page = new StandardPageBuilder().WithPrivate(true).Build();

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Private)) && x.Value.Equals(page.Private));
        }

        [Fact]
        public void GivenDateTimeProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<DateTime>), out var _).Returns(x =>
            {
                x[1] = new DateTimePropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);
            var expectedStartingDate = new DateTime(3000, 1,1);
            var page = new StandardPageBuilder().WithStarting(expectedStartingDate).Build();

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

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
            urlHelper.ContentUrl(contentReference, Arg.Any<IUrlSettings>()).Returns(contentReferencePageUrl);
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<ContentReference>), out var _).Returns(x =>
            {
                x[1] = new ContentReferencePropertyHandler(urlHelper, this._contentSerializerSettings);
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

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
            var contentReferencePropertyHandler = new ContentReferencePropertyHandler(urlHelper, this._contentSerializerSettings);
            urlHelper.ContentUrl(pageReference, Arg.Any<IUrlSettings>()).Returns(pageReferenceUrl);
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<PageReference>), out var _).Returns(x =>
            {
                x[1] = new PageReferencePropertyHandler(contentReferencePropertyHandler);
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.PageReference)) && x.Value.Equals(pageReferenceUrl));
        }

        [Fact]
        public void GivenContentAreaProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var contentArea = CreateContentArea();
            var page = new StandardPageBuilder().WithMainContentArea(contentArea).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<ContentArea>), out var _).Returns(x =>
            {
                x[1] = new ContentAreaPropertyHandler(this._contentLoader, this._sut, this._contentSerializerSettings);
                return true;
            });
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<string>), out var _).Returns(x =>
            {
                var selectStrategy = new DefaultSelectStrategy();
                x[1] = new StringPropertyHandler(selectStrategy, selectStrategy);
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContainKey(nameof(StandardPage.MainContentArea));
        }

        [Fact]
        public void GivenStringArrayProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var strings = new[] {"any", "value"};
            var page = new StandardPageBuilder().WithStrings(strings).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<IEnumerable<string>>), out var _).Returns(x =>
            {
                x[1] = new StringListPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Strings)) && x.Value.Equals(strings));
        }

        [Fact]
        public void GivenStringListProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var strings = new List<string> { "any", "value" };
            var page = new StandardPageBuilder().WithStrings(strings).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<IEnumerable<string>>), out var _).Returns(x =>
            {
                x[1] = new StringListPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Strings)) && x.Value.Equals(strings));
        }

        [Fact]
        public void GivenIntListProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var ints = new List<int> { 1000, 2000 };
            var page = new StandardPageBuilder().WithInts(ints).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<IEnumerable<int>>), out var _).Returns(x =>
            {
                x[1] = new IntListPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Ints)) && x.Value.Equals(ints));
        }

        [Fact]
        public void GivenDoubleListProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var doubles = new List<double> { 1000, 2000.50 };
            var page = new StandardPageBuilder().WithDoubles(doubles).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<IEnumerable<double>>), out var _).Returns(x =>
            {
                x[1] = new DoubleListPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.Doubles)) && x.Value.Equals(doubles));
        }

        [Fact]
        public void GivenDateTimeListProperty_WhenGetStructuredData_ThenReturnsCorrectValue()
        {
            var dateTimes = new List<DateTime> { new DateTime(2000, 1, 12), new DateTime(3000, 1 ,20) };
            var page = new StandardPageBuilder().WithDateTimes(dateTimes).Build();
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.TryGetExistingInstance(typeof(IPropertyHandler<IEnumerable<DateTime>>), out var _).Returns(x =>
            {
                x[1] = new DateTimeListPropertyHandler();
                return true;
            });
            ServiceLocator.SetLocator(serviceLocator);

            var result = this._sut.GetStructuredData(page, this._contentSerializerSettings);

            result.ShouldContain(x => x.Key.Equals(nameof(StandardPage.DateTimes)) && x.Value.Equals(dateTimes));
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
            contentArea.FilteredItems.Returns(items);
            
            return contentArea;
        }
    }
}
