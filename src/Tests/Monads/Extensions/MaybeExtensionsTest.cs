using Pocket.Common.Extensions;
using Xunit;

namespace Pocket.Common.Tests.Monads.Extensions
{
    public class MaybeExtensionsTest
    {
        [Fact]
        public void Maybe_ShouldReturnNothing_IfSelfIsNull() =>
            Assert.True(((string) null).Maybe().IsNothing);
        
        [Fact]
        public void Maybe_ShouldReturnJust_IfSelfIsNotNull() =>
            Assert.True("1".Maybe().HasValue);
        
        [Fact]
        public void Just_ShouldReturnJust() =>
            Assert.True(5.Just().HasValue);

        [Fact]
        public void Or_ShouldReturnValue_IfHasValue() => Assert.Equal("", "".Maybe().Or("5"));

        [Fact]
        public void Or_ShouldReturnDefaultValue_IfNothing() => Assert.Equal("5", ((string)null).Maybe().Or("5"));

    }
}