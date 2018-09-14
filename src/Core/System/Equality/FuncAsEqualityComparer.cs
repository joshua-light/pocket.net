using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    public class FuncAsEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _func;

        public FuncAsEqualityComparer(Func<T, T, bool> func)
        {
            func.EnsureNotNull();
            
            _func = func;
        }

        public bool Equals(T x, T y) => _func(x, y);
        public int GetHashCode(T obj) => obj.GetHashCode();
    }
}