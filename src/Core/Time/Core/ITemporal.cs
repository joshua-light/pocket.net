using System;

namespace Pocket.Common.Time
{
    public interface ITemporal
    {
        void Exist(TimeSpan span);
    }
}