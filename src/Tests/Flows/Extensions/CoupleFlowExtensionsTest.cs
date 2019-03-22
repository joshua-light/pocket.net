using System;
using NSubstitute;
using Pocket.Common.Flows;
using Xunit;

namespace Pocket.Common.Tests.Flows.Extensions
{
    public class CoupleFlowExtensionsTest
    {
        [Fact]
        public void PulsedOnNext_ShouldDispatchAndCallOnNext()
        {
            var source = Substitute.For<IFlow<(int, string)>>();
            var action = Substitute.For<Action<int, string>>();

            source.PulsedOnNext(action);

            action.Received(1).Invoke(Arg.Any<int>(), Arg.Any<string>());
            source.Received(1).OnNext(Arg.Any<Action<(int, string)>>());
        }
    }
}