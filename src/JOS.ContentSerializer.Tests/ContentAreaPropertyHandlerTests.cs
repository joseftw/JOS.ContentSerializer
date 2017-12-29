using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using JOS.ContentSerializer.Internal.Default;
using JOS.ContentSerializer.Tests.Pages;
using NSubstitute;

namespace JOS.ContentSerializer.Tests
{
    public class ContentAreaPropertyHandlerTests
    {
        private readonly ContentAreaPropertyHandler _sut;

        public ContentAreaPropertyHandlerTests()
        {
            var contentLoader = Substitute.For<IContentLoader>();
            SetupContentLoader(contentLoader);
            var propertyManager = Substitute.For<IPropertyManager>();
            this._sut = new ContentAreaPropertyHandler(contentLoader, propertyManager, new ContentSerializerSettings());
        }

        // TODO TESTS

        private static void SetupContentLoader(IContentLoader contentLoader)
        {
            contentLoader.Get<ContentData>(new ContentReference(1000))
                .Returns(new VideoBlock
                {
                    Name = "My name",
                    Url = new Url("https://josef.guru")
                });
        }

        private static ContentArea CreateContentArea(IEnumerable<ContentReference> content)
        {
            var contentArea = Substitute.For<ContentArea>();
            var items = content.Select(x => new ContentAreaItem
            {
                ContentLink = x
            }).ToList();

            contentArea.Items.Returns(items);
            contentArea.Count.Returns(items.Count);
            contentArea.Items.Returns(items);

            return contentArea;
        }
    }
}
