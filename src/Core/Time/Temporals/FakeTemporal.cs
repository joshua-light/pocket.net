using System;

namespace Pocket.Common.Time
{
    public class FakeTemporal : ITemporal
    {
        public void Exist(TimeSpan span) { }
    }
}