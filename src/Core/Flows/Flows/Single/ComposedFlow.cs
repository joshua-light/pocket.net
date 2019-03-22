using System;

namespace Pocket.Common.Flows
{
    internal class ComposedFlow<T1, T2> : IFlow<(T1, T2)>
    {
        private event Action<(T1, T2)> Next;
        
        private readonly IFlow<T1> _a;
        private readonly IFlow<T2> _b;
        
        public ComposedFlow(IFlow<T1> a, IFlow<T2> b)
        {
            _a = a;
            _b = b;
            
            a.OnNext(x => Next?.Invoke((x, b.Current)));
            b.OnNext(x => Next?.Invoke((a.Current, x)));
        }

        public (T1, T2) Current => (_a.Current, _b.Current);

        public IDisposable OnNext(Action<(T1, T2)> action) =>
            new CompactDisposable(
                () => Next += action,
                () => Next -= action);

        
    }
}