namespace Pocket.Common.Flows
{
    public interface IVoidFlux : IVoidFlow
    {
        void Pulse();
    }
}