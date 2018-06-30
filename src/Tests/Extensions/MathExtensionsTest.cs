using Xunit;

namespace Pocket.Common.Tests.Extensions
{
  public class MathExtensionsTest
  {
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