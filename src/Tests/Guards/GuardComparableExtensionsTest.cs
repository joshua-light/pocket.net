using System;
using Xunit;

namespace Pocket.Common.Tests.Guards
{
    public class GuardComparableExtensionsTest
    {
        #region EnsureBetween

        [Theory]
        [InlineData(0, 100, -1)]
        [InlineData(0, 100, -2)]
        [InlineData(0, 100, -10)]
        [InlineData(0, 100, -100)]
        [InlineData(0, 100, int.MinValue)]
        public void EnsureBetween_ShouldThrowGuardException_IfValueIsLessThanLowerBound(int min, int max, int value) =>
            Assert.Throws<ArgumentException>(() => value.EnsureBetween(min, max));

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(0, 1, 3)]
        [InlineData(0, 1, 10)]
        [InlineData(0, 1, 100)]
        [InlineData(0, 1, int.MaxValue)]
        public void EnsureBetween_ShouldThrowGuardException_IfValueIsGreaterThanLowerBound(int min, int max, int value) =>
            Assert.Throws<ArgumentException>(() => value.EnsureBetween(min, max));

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(0, 100, 1)]
        [InlineData(0, 100, 50)]
        [InlineData(0, 100, 98)]
        [InlineData(0, 100, 99)]
        [InlineData(0, 100, 100)]
        public void EnsureBetween_ShouldNotThrow_IfValueIsBetweenInclusively(int min, int max, int value) =>
            value.EnsureBetween(min, max);

        #endregion

        #region EnsureLess

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 10)]
        [InlineData(1, 100)]
        [InlineData(1, int.MaxValue)]
        public void EnsureLess_ShouldThrowGuardException_IfValueIsGreaterOrEqual(int bound, int value) =>
            Assert.Throws<ArgumentException>(() => value.EnsureLess(bound));

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(1, -10)]
        [InlineData(1, -100)]
        [InlineData(1, int.MinValue)]
        public void EnsureLess_ShouldNotThrow_IfValueIsLess(int bound, int value) =>
            value.EnsureLess(bound);

        #endregion

        #region EnsureLessOrEqual
        
        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 10)]
        [InlineData(1, 100)]
        [InlineData(1, 1000)]
        [InlineData(1, int.MaxValue)]
        public void EnsureLessOrEqual_ShouldThrowGuardException_IfValueIsGreater(int bound, int value) =>
            Assert.Throws<ArgumentException>(() => value.EnsureLessOrEqual(bound));

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(1, -10)]
        [InlineData(1, int.MinValue)]
        public void EnsureLessOrEqual_ShouldNotThrow_IfValueIsLessOrEqual(int bound, int value) =>
            value.EnsureLessOrEqual(bound);
        
        #endregion

        #region EnsureGreater

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(1, -10)]
        [InlineData(1, int.MinValue)]
        public void EnsureGreater_ShouldThrowGuardException_IfValueIsLessOrEqual(int bound, int value) =>
            Assert.Throws<ArgumentException>(() => value.EnsureGreater(bound));

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 10)]
        [InlineData(1, 100)]
        [InlineData(1, 1000)]
        [InlineData(1, int.MaxValue)]
        public void EnsureGreater_ShouldNotThrow_IfValueIsGreater(int bound, int value) =>
            value.EnsureGreater(bound);

        #endregion

        #region EnsureGreaterOrEqual

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(1, -10)]
        [InlineData(1, -100)]
        [InlineData(1, int.MinValue)]
        public void EnsureGreaterOrEqual_ShouldThrowGuardException_IfValueIsLess(int bound, int value) =>
            Assert.Throws<ArgumentException>(() => value.EnsureGreaterOrEqual(bound));

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 10)]
        [InlineData(1, 100)]
        [InlineData(1, int.MaxValue)]
        public void EnsureGreaterOrEqual_ShouldNotThrow_IfValueIsGreaterOrEqual(int bound, int value) =>
            value.EnsureGreaterOrEqual(bound);

        #endregion
    }
}