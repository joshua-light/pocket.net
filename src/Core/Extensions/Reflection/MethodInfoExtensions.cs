using System.Reflection;

namespace Pocket.Common
{
  public static class MethodInfoExtensions
  {
    public static ParameterInfo[] Arguments(this MethodInfo self) =>
      self.GetParameters();
  }
}