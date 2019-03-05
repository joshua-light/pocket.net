using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Monads
{
  public class ToggleTest
  {
    [Fact]
    public void Use_ShouldReturnCurrentValue_AndSwitchCurrentToOpposite()
    {
      var toggle = new Toggle<bool>(@default: false, or: true);

      toggle.Use().ShouldBe(false);
      toggle.Use().ShouldBe(true);
      toggle.Use().ShouldBe(false);
    }

    [Fact]
    public void Reset_ShouldSetOffValue()
    {
      var toggle = new Toggle<bool>(@default: false, or: true);

      toggle.Use().ShouldBe(false);
      toggle.Reset();
      toggle.Use().ShouldBe(false);
    }

    [Fact]
    public void UseAndReset_ShouldReturnCurrentValue_AndThenResetItToOff()
    {
      var toggle = new Toggle<bool>(@default: false, or: true);

      toggle.UseAndReset().ShouldBe(false);
      toggle.UseAndReset().ShouldBe(false);
      toggle.UseAndReset().ShouldBe(false);
    }
  }
}