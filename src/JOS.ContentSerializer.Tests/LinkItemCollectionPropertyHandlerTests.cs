using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.SpecializedProperties;
using JOS.ContentSerializer.Internal.Default;
using NSubstitute;
using Shouldly;
using Xunit;
using LinkItem = JOS.ContentSerializer.Internal.LinkItem;

namespace JOS.ContentSerializer.Tests
{
    public class LinkItemCollectionPropertyHandlerTests
    {
        private readonly LinkItemCollectionPropertyHandler _sut;
        private readonly IUrlHelper _urlHelper;
        private IContentSerializerSettings _contentSerializerSettings;

        public LinkItemCollectionPropertyHandlerTests()
        {
            this._contentSerializerSettings = Substitute.For<IContentSerializerSettings>();
            this._contentSerializerSettings.UrlSettings = new UrlSettings();
            this._urlHelper = Substitute.For<IUrlHelper>();
            this._sut = new LinkItemCollectionPropertyHandler(this._urlHelper);
        }

        [Fact]
        public void GivenNullLinkItemCollection_WhenHandle_ThenReturnsNull()
        {
            var result = this._sut.Handle(null, null, null, this._contentSerializerSettings);

            result.ShouldBeNull();
        }

        [Fact]
        public void GivenMailToLink_WhenHandle_ThenReturnsCorrectMailToLink()
        {
            var value = "mailto:mail@example.com";
            var linkItemCollection = new LinkItemCollection();
            linkItemCollection.Add(new EPiServer.SpecializedProperties.LinkItem
            {
                Href = value,
                Text = "any text",
                Title = "any title"
            });

            var result = ((IEnumerable<LinkItem>)this._sut.Handle(linkItemCollection, null, null, this._contentSerializerSettings)).ToList();

            result.Count.ShouldBe(1);
            result.ShouldContain(x => x.Href == value);
            result.ShouldContain(x => x.Text == "any text");
            result.ShouldContain(x => x.Title == "any title");
        }

        [Fact]
        public void GivenExternalLink_WhenHandle_ThenReturnsCorrectLink()
        {
            var value = "https://example.com/anypage?query=value";
            var linkItemCollection = new LinkItemCollection();
            linkItemCollection.Add(new EPiServer.SpecializedProperties.LinkItem
            {
                Href = value,
                Text = "any text",
                Title = "any title",
                Target = "_blank"
            });

            var result = ((IEnumerable<LinkItem>)this._sut.Handle(linkItemCollection, null, null, this._contentSerializerSettings)).ToList();

            result.Count.ShouldBe(1);
            result.ShouldContain(x => x.Href == value);
            result.ShouldContain(x => x.Text == "any text");
            result.ShouldContain(x => x.Title == "any title");
            result.ShouldContain(x => x.Target == "_blank");
        }

        [Fact]
        public void GivenInternalLink_WhenHandle_ThenReturnsCorrectAbsoluteLink()
        {
            var value = "random-internallink";
            var linkItemCollection = new LinkItemCollection();
            var expected = "https://example.com/my-pretty-url/?anyQuery=hej";
            this._urlHelper.ContentUrl(Arg.Any<Url>(), Arg.Any<UrlSettings>())
                .Returns(expected);
            var linkItem = Substitute.For<EPiServer.SpecializedProperties.LinkItem>();
            linkItem.Href.Returns(value);
            linkItem.Text.Returns("any text");
            linkItem.Title.Returns("any title");
            linkItem.Target.Returns("_blank");
            linkItem.ReferencedPermanentLinkIds.Returns(new List<Guid>
            {
                Guid.NewGuid()
            });
            linkItemCollection.Add(linkItem);

            var result = ((IEnumerable<LinkItem>)this._sut.Handle(linkItemCollection, null, null, this._contentSerializerSettings)).ToList();

            result.Count.ShouldBe(1);
            result.ShouldContain(x => x.Href == expected);
            result.ShouldContain(x => x.Text == "any text");
            result.ShouldContain(x => x.Title == "any title");
            result.ShouldContain(x => x.Target == "_blank");
        }

        [Fact]
        public void GivenInternalLink_WhenHandleWithUseAbsoluteUrlsSetToFalse_ThenReturnsCorrectRelativeLink()
        {
            var value = "random-internallink";
            var linkItemCollection = new LinkItemCollection();
            var expected = "/my-pretty-url/?anyQuery=hej";
            this._contentSerializerSettings.UrlSettings.Returns(new UrlSettings { UseAbsoluteUrls = false });
            this._urlHelper.ContentUrl(Arg.Any<Url>(), this._contentSerializerSettings.UrlSettings).Returns(expected);
            var linkItem = Substitute.For<EPiServer.SpecializedProperties.LinkItem>();
            linkItem.Href.Returns(value);
            linkItem.Text.Returns("any text");
            linkItem.Title.Returns("any title");
            linkItem.Target.Returns("_blank");
            linkItem.ReferencedPermanentLinkIds.Returns(new List<Guid>
            {
                Guid.NewGuid()
            });
            linkItemCollection.Add(linkItem);

            var result = ((IEnumerable<LinkItem>)this._sut.Handle(linkItemCollection, null, null, this._contentSerializerSettings)).ToList();

            result.Count.ShouldBe(1);
            result.ShouldContain(x => x.Href == expected);
            result.ShouldContain(x => x.Text == "any text");
            result.ShouldContain(x => x.Title == "any title");
            result.ShouldContain(x => x.Target == "_blank");
        }
    }
}
