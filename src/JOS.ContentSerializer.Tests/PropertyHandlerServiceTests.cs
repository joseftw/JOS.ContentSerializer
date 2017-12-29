using System.Reflection;
using System.Runtime.InteropServices;
using JOS.ContentSerializer.Internal;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.ContentSerializer.Tests
{
    public class PropertyHandlerServiceTests
    {
        private readonly PropertyHandlerService _sut;

        public PropertyHandlerServiceTests()
        {
            this._sut = new PropertyHandlerService();
        }

        [Fact]
        public void GivenNullPropertyInfo_WhenGetPropertyHandler_ThenReturnsNull()
        {
            var result = this._sut.GetPropertyHandler(null);

            result.ShouldBeNull();
        }
    }
}
