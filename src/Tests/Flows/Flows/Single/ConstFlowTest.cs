using System;
using NSubstitute;
using Pocket.Flows;
using Xunit;

namespace Pocket.Tests.Flows.Flows.Single
{
    public class ConstFlowTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(45)]
        public void Current_ShouldEqualToConstructorValue(int value) =>
            Assert.Equal(value, new ConstFlow<int>(value).Current);

        [Fact]
        public void Current_ShouldEqualToNull_IfParameterlessCtorWasUsed() =>
            Assert.Null(new ConstFlow<string>().Current);

        [Fact]
        public void OnNext_ShouldNotInvokeAction()
        {
            var action = Substitute.For<Action<int>>();
            var flow = new ConstFlow<int>(5);

            flow.OnNext(action);
            
            action.DidNotReceive().Invoke(Arg.Any<int>());
        }
    }
}