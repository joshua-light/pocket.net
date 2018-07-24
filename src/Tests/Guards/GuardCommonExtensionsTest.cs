using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Pocket.Common.Tests.Guards
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
        public void EnsureNotNull_ShouldThrowGuardException_IfValueIsNull() =>
            Assert.Throws<ArgumentNullException>(() => ((object) null).EnsureNotNull());

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        public void EnsureNotNull_ShouldNotThrow_IfValueIsNotNull(object value) =>
            value.EnsureNotNull();

        #endregion
        
        #region EnsureNotEqual

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(int.MaxValue, int.MaxValue)]
        [InlineData(int.MinValue, int.MinValue)]
        public void EnsureNotEqual_ShouldThrowGuardException_IfValuesAreEqual(int a, int b) =>
            Assert.Throws<ArgumentException>(() => a.EnsureNotEqual(b));

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(int.MaxValue, int.MinValue)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void EnsureNotEqual_ShouldNotThrowGuardException_IfValuesAreDifferent(int a, int b) =>
            a.EnsureNotEqual(b);

        #endregion
        
        #region EnsureEqual

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(int.MaxValue, int.MinValue)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void EnsureEqual_ShouldThrowGuardException_IfValuesAreDifferent(int a, int b) =>
            Assert.Throws<ArgumentException>(() => a.EnsureEqual(b));

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(int.MaxValue, int.MaxValue)]
        [InlineData(int.MinValue, int.MinValue)]
        public void EnsureEqual_ShouldNotThrowGuardException_IfValuesAreDifferent(int a, int b) =>
            a.EnsureEqual(b);

        #endregion
    }
}