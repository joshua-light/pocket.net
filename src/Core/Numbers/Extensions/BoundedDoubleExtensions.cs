namespace Pocket.Common.Numbers.Extensions
{
    public static class BoundedDoubleExtensions
    {
        public static BoundedDouble BoundedBy(this int self, int max) => BoundedBy((double) self, max);
        public static BoundedDouble BoundedBy(this double self, double max) =>
            new BoundedDouble(self, max);

        public static double Percent(this BoundedDouble self) =>
            self.Value / self.Max * 100;

        public static BoundedDouble Increase(this BoundedDouble self, double value) =>
            self.WithCurrent(self.Value + value);
        public static BoundedDouble IncreaseMax(this BoundedDouble self, double value) =>
            self.WithMax(self.Max + value);

        public static BoundedDouble Decrease(this BoundedDouble self, double value) =>
            self.WithCurrent(self.Value - value);
        public static BoundedDouble DecreaseMax(this BoundedDouble self, double value) =>
            self.WithMax(self.Max - value);
    }
}