using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
  public class ToggleTest
  {
    [Fact]
    public void Use_ShouldReturnFirstValue_IfCalledOddTimes()
    {
      var toggle = new Toggle<bool>(off: false, on: true);
      
      for (var i = 1; i <= 10; i++)
      {
        var value = toggle.Use();
        
        if (i % 2 != 0)
          value.ShouldBe(false);
      }
    }

    [Fact]
    public void Use_ShouldReturnSecondValue_IfCalledEvenTimes()
    {
      var toggle = new Toggle<bool>(off: false, on: true);
      
      for (var i = 1; i <= 10; i++)
      {
        var value = toggle.Use();
        
        if (i % 2 == 0)
          value.ShouldBe(true);
      }
    }

    [Fact]
    public void Reset_ShouldSetOffValue()
    {
      var toggle = new Toggle<bool>(off: false, on: true);
      
      toggle.Reset();
      toggle.Use().ShouldBe(false);
      toggle.Reset();
      toggle.Use().ShouldBe(false);
      toggle.Reset();
      toggle.Use().ShouldBe(false);
    }
  }
}