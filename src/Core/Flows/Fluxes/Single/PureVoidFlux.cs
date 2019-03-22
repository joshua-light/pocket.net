using System;

namespace Pocket.Common.Flows
{
    public sealed class PureVoidFlux : IVoidFlux
    {
        private event Action Next;

        public IDisposable OnNext(Action action)
        {
            action.EnsureNotNull();
            
            return new CompactDisposable(
                () => Next += action,
                () => Next -= action);
        }

        public void Pulse() => Next?.Invoke();
    }
}