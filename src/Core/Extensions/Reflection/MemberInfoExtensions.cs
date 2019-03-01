using System;
using System.Reflection;

namespace Pocket.Common
{
    public static class MemberInfoExtensions
    {
        public static bool Has<T>(this MemberInfo self) where T : Attribute =>
            self.Attribute<T>() != null;

        public static T Attribute<T>(this MemberInfo self) where T : Attribute =>
            self.GetCustomAttribute<T>();
    }
}