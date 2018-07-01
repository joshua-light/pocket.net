using System;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
  public class MathExtensionsTest
  {
    #region Abs

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(short.MaxValue)]
    [InlineData(short.MinValue + 1)]
    public void Abs_ShouldBehaveAsMathAbs_IfShort(short value) => Assert.Equal(Math.Abs(value), value.Abs());
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue + 1)]
    public void Abs_ShouldBehaveAsMathAbs_IfInt(int value) => Assert.Equal(Math.Abs(value), value.Abs());
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(long.MaxValue)]
    [InlineData(long.MinValue + 1)]
    public void Abs_ShouldBehaveAsMathAbs_IfLong(long value) => Assert.Equal(Math.Abs(value), value.Abs());
    
    [Theory]
    [InlineData(0f)]
    [InlineData(1.1f)]
    [InlineData(-1.1f)]
    [InlineData(float.MaxValue)]
    [InlineData(float.MinValue + 1)]
    public void Abs_ShouldBehaveAsMathAbs_IfFloat(float value) => Assert.Equal(Math.Abs(value), value.Abs());
    
    [Theory]
    [InlineData(0.0)]
    [InlineData(1.1)]
    [InlineData(-1.1)]
    [InlineData(double.MaxValue)]
    [InlineData(double.MinValue + 1)]
    public void Abs_ShouldBehaveAsMathAbs_IfDouble(double value) => Assert.Equal(Math.Abs(value), value.Abs());
    
//    This is not working at a time due to some implicit conversion error along with decimal unavailable constants.
//    [Theory]
//    [InlineData(0.0)]
//    [InlineData(1.1)]
//    [InlineData(-1.1)]
//    [InlineData(double.MaxValue)]
//    [InlineData(double.MinValue + 1)]
//    public void Abs_ShouldBehaveAsMathAbs_IfDecimal(decimal value) => Assert.Equal(Math.Abs(value), value.Abs());

    #endregion
    
    public class OrCoupleTest
    {
      [Theory]
      [InlineData(0, 10)]
      [InlineData(0, 9)]
      [InlineData(0, 100)]
      [InlineData(-10, -9)]
      public void IfLess_ShouldReturnSecondValue_IfFirstIsLessThanSecond(int first, int second) =>
        Assert.Equal(second, new MathExtensions.OrCouple<int>(first, second).IfLess());
      
      [Theory]
      [InlineData(0, 0)]
      [InlineData(1, 0)]
      [InlineData(2, 0)]
      [InlineData(10, 0)]
      [InlineData(15, 0)]
      public void IfLess_ShouldReturnFirstValue_IfFirstIsGreaterThanSecond(int first, int second) =>
        Assert.Equal(first, new MathExtensions.OrCouple<int>(first, second).IfLess());
      
      [Theory]
      [InlineData(0, 0)]
      [InlineData(1, 0)]
      [InlineData(2, 0)]
      [InlineData(10, 0)]
      [InlineData(15, 0)]
      public void IfGreater_ShouldReturnSecondValue_IfFirstIsGreaterThanSecond(int first, int second) =>
        Assert.Equal(second, new MathExtensions.OrCouple<int>(first, second).IfGreater());
      
      [Theory]
      [InlineData(0, 10)]
      [InlineData(0, 9)]
      [InlineData(0, 100)]
      [InlineData(-10, -9)]
      public void IfGreater_ShouldReturnFirstValue_IfFirstIsLessThanSecond(int first, int second) =>
        Assert.Equal(first, new MathExtensions.OrCouple<int>(first, second).IfGreater());
    }
  }
}