using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Pocket.Common.Tests.Extensions
{
    public class TaskExtensionsTest
    {
        public class SimpleTask
        {
            [Fact]
            public async void WithTimeout_ShouldReturnSuccess_IfTaskWasCompleted() =>
                Assert.NotNull(await TaskDelay(100).WithTimeout(200));

            [Fact]
            public async void WithTimeout_ShouldReturnFail_IfTaskWasEndedByTimeout() =>
                Assert.Null(await TaskDelay(200).WithTimeout(100));

            private static async Task<List<int>> TaskDelay(int n)
            {
                await Task.Delay(n);
                return new List<int>();
            }
        }
        
        public class WithTimeoutResult
        {
            [Fact]
            public async void WithTimeout_ShouldReturnSuccess_IfTaskWasCompleted()
            {
                var result = await TaskDelay(300).WithTimeout(500);
                Assert.True(result.Success);
            }

            [Fact]
            public async void WithTimeout_ShouldReturnFail_IfTaskWasEndedByTimeout()
            {
                var result = await TaskDelay(1000).WithTimeout(500);
                Assert.True(result.Fail);
            }

            private static async Task<Result> TaskDelay(int n)
            {
                await Task.Delay(n);
                return Result.Succeded();
            }
        }

        public class WithTimeoutGenericResult
        {
            [Fact]
            public async void WithTimeout_ShouldReturnSuccess_IfTaskWasCompleted()
            {
                var result = await TaskDelayWithCourier(300).WithTimeout(500);
                Assert.True(result.Success);
            }
        
            [Fact]
            public async void WithTimeout_ShouldReturnFail_IfTaskWasEndedByTimeout()
            {
                var result = await TaskDelayWithCourier(1000).WithTimeout(500);
                Assert.True(result.Fail);
            }
        
            private static async Task<Result<int>> TaskDelayWithCourier(int n)
            {
                await Task.Delay(n);
                return Result.Succeded(0);
            }
        }
    }
}