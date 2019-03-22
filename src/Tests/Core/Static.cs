using System;

namespace Pocket.Common.Tests.Core
{
    public static class Static
    {
        public static Action Call(Action of) => of;
    }
}