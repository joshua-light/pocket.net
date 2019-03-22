using System;

namespace Pocket.Common.Time
{
    public class CompositeTemporal : ITemporal
    {
        private readonly ITemporal _a;
        private readonly ITemporal _b;

        public CompositeTemporal(ITemporal a, ITemporal b)
        {
            _a = a;
            _b = b;
        }

        public void Exist(TimeSpan span)
        {
            _a.Exist(span);
            _b.Exist(span);
        }
    }
}