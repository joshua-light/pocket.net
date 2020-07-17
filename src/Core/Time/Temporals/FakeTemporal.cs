using System;

namespace Pocket.Time
{
    public class FakeTemporal : ITemporal
    {
        public void Exist(TimeSpan span) { }
    }
}