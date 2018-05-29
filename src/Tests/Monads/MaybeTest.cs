using System;
using Pocket.Common.Extensions;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
    public class MaybeTest
    {
        public class ReferenceType
        {
            [Fact]
            public void IsNothingAndHasValue_ShouldReturnCorrectResult()
            {
                Assert.True(((string) null).Maybe() == Maybe<string>.Nothing);
                Assert.True(((string) null).Maybe().IsNothing);
                Assert.False(((string) null).Maybe().HasValue);

                Assert.True("1".Maybe() != Maybe<string>.Nothing);
                Assert.True("1".Maybe().HasValue);
                Assert.False("1".Maybe().IsNothing);
            }
            
            [Theory]
            [InlineData("1", "1")]
            [InlineData("12", "12")]
            [InlineData("123123", "123123")]
            public void Equals_ShouldReturnCorrectResult(string x, string y)
            {
                var a = x.Maybe();
                var b = y.Maybe();
                
                Assert.True(a.Equals(b));
                Assert.True(b.Equals(a));
                Assert.True(b.Equals((object) a));
                Assert.True(b.Equals((object) a));
                Assert.True(a == b);
                Assert.True(b == a);
                Assert.True(a.GetHashCode() == b.GetHashCode());
            }

            [Theory]
            [InlineData("Test")]
            [InlineData("Test123")]
            [InlineData("Test12312354123123123154123123")]
            public void GetHashCode_ShouldBeTheSameAsForValue(string data) =>
                Assert.Equal(data.GetHashCode(), data.Maybe().GetHashCode());

            [Theory]
            [InlineData("Test")]
            [InlineData("Test123")]
            [InlineData("Test12312354123123123154123123")]
            public void Value_ShouldReturnCorrectValue(string value) =>
                Assert.Equal(value, value.Maybe().Value);
            
            [Fact]
            public void Value_ShouldThrowInvalidOperationException_IfNothing() =>
                Assert.Throws<InvalidOperationException>(() => ((string)null).Maybe().Value);
        }

        public class ValueType
        {
            [Fact]
            public void DefaultConstructor_ShouldCreateNothing() =>
                Assert.True(new Maybe<int>().IsNothing);

            [Fact]
            public void Just_ShouldCreateNotNothing() =>
                Assert.False(5.Just().IsNothing);
        }
    }
}