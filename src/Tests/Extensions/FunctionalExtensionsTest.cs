using System;
using NSubstitute;
using Pocket.Extensions;
using Xunit;

namespace Pocket.Tests.Extensions
{
    public class FunctionalExtensionsTest
    {
        public class As
        {
            [Fact]
            public void As_ShouldApplyMapFunction() =>
                Assert.Equal(10, 1.As(x => x + 1).As(x => x * 10).As(x => x / 2));
                        
            [Fact]
            public void As_ShouldThrowException_IfMapIsNull() =>
                Assert.Throws<ArgumentNullException>(() => "".As<string, int>(null));
        }

        public class Do
        {
            [Fact]
            public void Do_ShouldApplyAction()
            {
                var action = Substitute.For<Action<string>>();

                "".Do(action);

                action.Received(1).Invoke("");
            }
            
            [Fact]
            public void Do_ShouldPropagate_IfActionIsNull() => "".Do(null);
        }
    }
}