using System;

namespace Pocket.Tests.Core
{
    public static class Static
    {
        public static Action Call(Action of) => of;
    }
}