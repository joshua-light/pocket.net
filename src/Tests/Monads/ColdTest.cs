using System;
using Xunit;

namespace Pocket.Tests.Monads
{
    public class ColdTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Value_ShouldBeEqualToFreezed(int value) =>
            Assert.Equal(value, new Cold<int>().Freeze(value).Value);

        [Fact]
        public void Value_ShouldThrowInvalidOperationException_IfItIsUnfreezed() =>
            Assert.Throws<InvalidOperationException>(() => new Cold<int>().Value);

        [Fact]
        public void Freeze_ShouldThrowInvalidOperationException_IfCalledTwice() =>
            Assert.Throws<InvalidOperationException>(() => new Cold<int>().Freeze(1).Freeze(1));
    }
}