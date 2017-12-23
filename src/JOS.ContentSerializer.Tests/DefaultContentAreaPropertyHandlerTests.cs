using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using JOS.ContentSerializer.Internal;
using JOS.ContentSerializer.Tests.Pages;
using NSubstitute;

namespace JOS.ContentSerializer.Tests
{
    public class DefaultContentAreaPropertyHandlerTests
    {
        private readonly DefaultContentAreaPropertyHandler _sut;

        public DefaultContentAreaPropertyHandlerTests()
        {
            var contentLoader = Substitute.For<IContentLoader>();
            SetupContentLoader(contentLoader);
            this._sut = new DefaultContentAreaPropertyHandler(contentLoader);
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
