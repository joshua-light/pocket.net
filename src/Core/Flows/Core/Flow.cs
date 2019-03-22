namespace Pocket.Common.Flows
{
    public static class Flow
    {
        public static IFlow<T> Empty<T>() => ConstFlow<T>.Empty;
    }
}