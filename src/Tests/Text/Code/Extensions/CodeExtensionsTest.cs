using Shouldly;
using Xunit;

namespace Pocket.Tests.Text.Code.Extensions
{
  public class CodeExtensionsTest
  {
    [Fact]
    public void Text_ShouldChangeText_IfWhenIsTrue() =>
        Code().Text("1", when: true).ToString().ShouldBe("1");
    
    [Fact]
    public void Text_ShouldNotChangeText_IfWhenIsFalse() =>
        Code().Text("1", when: false).ToString().ShouldBe("");
    
    private static Pocket.Code Code() => new Pocket.Code();
  }
}