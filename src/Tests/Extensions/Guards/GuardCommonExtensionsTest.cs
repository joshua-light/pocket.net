using System;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Extensions.Guards
{
    public class GuardCommonExtensionsTest
    {
        #region Ensure

        [Theory]
        [InlineData(123)]
        [InlineData("123")]
        [InlineData(123.0)]
        public void Ensure_ShouldPass_IfPredicateReturnsTrue(object data) => data.Ensure(data.Equals);
        
        [Fact]
        public void Ensure_ShouldThrowArgumentNullException_IfSelfIsNull() =>
            Assert.Throws<ArgumentNullException>(() => ((string)null).Ensure(x => x == ""));
        
        [Fact]
        public void Ensure_ShouldThrowArgumentNullException_IfPredicateIsNull() =>
            Assert.Throws<ArgumentNullException>(() => 5.Ensure(null));

        [Fact]
        public void Ensure_ShouldThrowArgumentExceptionWithDefaultMessage_IfPredicateReturnsFalse()
        {
            var exception = Assert.Throws<ArgumentException>(() => 5.Ensure(x => x == 6));
            
            Assert.Equal("Specified predicate didn't match.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Message.")]
        [InlineData("Another message.")]
        public void Ensure_ShouldThrowArgumentExceptionWithSpecifiedMessage_IfPredicateReturnsFalse(string message)
        {
            var exception = Assert.Throws<ArgumentException>(() => 5.Ensure(x => x == 6, message));
            
            Assert.Equal(message, exception.Message);
        }
        
        #endregion
        
        #region EnsureNotNull

        [Fact]
        public void EnsureNotNull_ShouldThrowArgumentNullException_IfValueIsNull() =>
            Assert.Throws<ArgumentNullException>(() => ((object) null).EnsureNotNull());

        [Fact]
        public void EnsureNotNull_ShouldThrowArgumentNullExceptionWithMessage_IfValueIsNull() =>
            Assert.Throws<ArgumentNullException>(() => ((object) null).EnsureNotNull("Message."))
                .Message
                .ShouldStartWith("Message.");

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        public void EnsureNotNull_ShouldNotThrow_IfValueIsNotNull(object value) =>
            value.EnsureNotNull();

        #endregion
        
        #region EnsureNull

        [Fact]
        public void EnsureNull_ShouldThrowArgumentException_IfValueIsNotNull() =>
            Assert.Throws<ArgumentException>(() => "".EnsureNull());

        [Fact]
        public void EnsureNull_ShouldThrowArgumentExceptionWithMessage_IfValueIsNotNull() =>
            Assert.Throws<ArgumentException>(() => "".EnsureNull("Message."))
            .Message
            .ShouldStartWith("Message.");
        
        [Fact]
        public void EnsureNull_ShouldNotThrow_IfValueIsNull() =>
            ((object) null).EnsureNull();

        #endregion

        #region EnsureIsTest

        [Fact]
        public void EnsureIs_ShouldNotThrow_IfObjectIsInstanceOfSpecifiedType()
        {
            "1".EnsureIs<string>();
            111.EnsureIs<int>();
            1.1.EnsureIs<double>();
        }
        
        [Fact]
        public void EnsureIs_ShouldThrowArgumentException_IfObjectIsNotInstanceOfSpecifiedType()
        {
            Assert.Throws<ArgumentException>(() => 5.EnsureIs<double>());
        }

        #endregion
    }
}