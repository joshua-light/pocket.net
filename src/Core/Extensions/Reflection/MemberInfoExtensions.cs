using System;
using System.Reflection;

namespace Pocket.Common
{
    public static class MemberInfoExtensions
    {
        public static bool Has<T>(this MemberInfo self) where T : Attribute =>
            self.GetCustomAttribute<T>() != null;
    }
}