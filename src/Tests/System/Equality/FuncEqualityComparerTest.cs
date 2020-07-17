using System;
using NSubstitute;
using Pocket.System;
using Xunit;

namespace Pocket.Tests.System.Equality
{
    public class FuncEqualityComparerTest
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 10)]
        public void Equals_ShouldCallFunc(int a, int b)
        {
            var func = Substitute.For<Func<int, int, bool>>();
            var comparer = new FuncEqualityComparer<int>(func);

            comparer.Equals(a, b);

            func.Received(1).Invoke(a, b);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_IfFuncIsNull() =>
            Assert.Throws<ArgumentNullException>(() => new FuncEqualityComparer<int>(null));

        [Fact]
        public void GetHashCode_ShouldCallInnerGetHashCode()
        {
            var comparer = new FuncEqualityComparer<object>((x, y) => true);
            var str = Substitute.For<object>();

            comparer.GetHashCode(str);

            str.Received(1).GetHashCode();
        }
    }
}