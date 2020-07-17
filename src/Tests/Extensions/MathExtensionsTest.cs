using System;
using Xunit;

namespace Pocket.Tests.Extensions
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
  }
}