using System;

namespace Pocket.Time
{
    public interface ITemporal
    {
        void Exist(TimeSpan span);
    }
}