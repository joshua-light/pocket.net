using System;
using static Pocket.Guard;

namespace Pocket.Flows
{
    public sealed class PureVoidFlux : IVoidFlux
    {
        private event Action Next;

        public IDisposable OnNext(Action action)
        {
            Ensure(action).NotNull();
            
            return new CompactDisposable(
                () => Next += action,
                () => Next -= action);
        }

        public void Pulse() => Next?.Invoke();
    }
}