using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
  public class ToggleTest
  {
    [Fact]
    public void Use_ShouldReturnFirstValue_IfCalledOddTimes()
    {
      var toggle = new Toggle<bool>(a: true, b: false);
      
      for (var i = 1; i <= 10; i++)
      {
        var value = toggle.Use();
        
        if (i % 2 != 0)
          value.ShouldBe(true);
      }
    }

    [Fact]
    public void Use_ShouldReturnSecondValue_IfCalledEvenTimes()
    {
      var toggle = new Toggle<bool>(a: true, b: false);
      
      for (var i = 1; i <= 10; i++)
      {
        var value = toggle.Use();
        
        if (i % 2 == 0)
          value.ShouldBe(false);
      }
    }
  }
}