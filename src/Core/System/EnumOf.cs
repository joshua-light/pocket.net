using System;

namespace Pocket.Common
{
    public static class EnumOf<T> where T : struct
    {
        public static T[] Values { get; } = (T[]) Enum.GetValues(typeof(T));
    }
}