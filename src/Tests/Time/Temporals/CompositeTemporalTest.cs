using System;
using NSubstitute;
using Pocket.Time;
using Xunit;

namespace Pocket.Tests.Time.Temporals
{
    public class CompositeTemporalTest
    {
        [Fact]
        public void Exist_ShouldCallFirstThenSecond()
        {
            var span = TimeSpan.FromDays(150);
            var a = Substitute.For<ITemporal>();
            var b = Substitute.For<ITemporal>();
            var composite = a.With(b);

            composite.Exist(span);

            a.Received(1).Exist(span);
            b.Received(1).Exist(span);
        }
    }
}