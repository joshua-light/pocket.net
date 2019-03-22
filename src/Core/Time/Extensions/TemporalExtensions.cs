namespace Pocket.Common.Time
{
    public static class TemporalExtensions
    {
        public static ITemporal With(this ITemporal self, ITemporal other) =>
            new CompositeTemporal(self, other);
    }
}