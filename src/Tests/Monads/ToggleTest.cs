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

      toggle.Current.ShouldBe(false);
      toggle.Current.ShouldBe(true);
      toggle.Current.ShouldBe(false);
    }

    [Fact]
    public void Reset_ShouldSetDefaultValue()
    {
      var toggle = new Toggle<bool>(@default: false, or: true);

      toggle.Current.ShouldBe(false);
      toggle.Reset();
      toggle.Current.ShouldBe(false);
    }

    [Fact]
    public void Reset_ShouldReturnCurrentValue()
    {
      var toggle = new Toggle<bool>(@default: false, or: true);

      toggle.Reset().ShouldBe(false);
      toggle.Reset().ShouldBe(false);
      toggle.Reset().ShouldBe(false);
    }
  }
}