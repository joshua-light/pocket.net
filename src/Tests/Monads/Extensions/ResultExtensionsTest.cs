using System;
using NSubstitute;
using Pocket.Common.Tests.Core.Extensions;
using Shouldly;
using Xunit;

namespace Pocket.Common.Tests.Monads.Extensions
{
    public class ResultExtensionsTest
    {
        [Fact]
        void Or_ShouldReturnValue_IfResultIsSuccess() => "".AsResult().Or("1").ShouldBe("");
        [Fact]
        void Or_ShouldReturnDefault_IfResultIsFail() => ((string) null).AsResult().Or("").ShouldBe("");
        
        [Fact]
        void AsResult_ShouldFail_IfValueIsNull() => ((string) null).AsResult().ShouldFail();
        [Fact]
        void AsResult_ShouldSucceed_IfValueIsNotNull() => "".AsResult().ShouldSucceed();

        [Fact]
        void AsResultMaybe_ShouldFail_IfValueIsNull() => ((string) null).Maybe().AsResult().ShouldFail();
        [Fact]
        void AsResultMaybe_ShouldSucceed_IfValueIsNotNull() => "".Maybe().AsResult().ShouldSucceed();

        [Fact]
        void OnSuccess_ShouldCallAction_IfResultIsSucceeded() => Call(Result.Succeeded(), (x, y) => x.OnSuccess(y));
        [Fact]
        void OnSuccess_ShouldNotCallAction_IfResultIsFailed() => NotCall(Result.Failed(), (x, y) => x.OnSuccess(y));
        
        [Fact]
        void OnFail_ShouldCallAction_IfResultIsFailed() => Call(Result.Failed(), (x, y) => x.OnFail(y));
        [Fact]
        void OnFail_ShouldNotCallAction_IfResultIsSucceeded() => NotCall(Result.Succeeded(), (x, y) => x.OnFail(y));
        
        [Fact]
        void With_ShouldSucceed_IfResultIsSucceeded() => Result.Succeeded().With(5).ShouldSucceed();
        [Fact]
        void With_ShouldFail_IfResultIsFailed() => Result.Failed().With(5).ShouldFail();
        [Fact]
        void With_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeeded().With(5));
        
        [Fact]
        void WithFunc_ShouldSucceed_IfResultIsSucceeded() => Result.Succeeded().With(() => 5).ShouldSucceed();
        [Fact]
        void WithFunc_ShouldFail_IfResultIsFailed() => Result.Failed().With(() => 5).ShouldFail();
        [Fact]
        void WithFunc_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeeded().With(() => 5));
        
        [Fact]
        void WithMaybe_ShouldSucceed_IfResultIsSucceeded() => Result.Succeeded().With(5.Just()).ShouldSucceed();
        [Fact]
        void WithMaybe_ShouldFail_IfResultIsFailed() => Result.Failed().With(5.Just()).ShouldFail();
        [Fact]
        void WithMaybe_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeeded().With(5.Just()));
        
        [Fact]
        void WithMaybeFunc_ShouldSucceed_IfResultIsSucceeded() => Result.Succeeded().With(() => 5.Just()).ShouldSucceed();
        [Fact]
        void WithMaybeFunc_ShouldFail_IfResultIsFailed() => Result.Failed().With(() => 5.Just()).ShouldFail();
        [Fact]
        void WithMaybeFunc_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeeded().With(() => 5.Just()));
        
        [Fact]
        void WithGenericResult_ShouldSucceed_IfBothSucceeded() => Result.Succeeded().With(Result.Succeeded(5)).ShouldSucceed();
        [Fact]
        void WithGenericResult_ShouldFail_IfFirstIsFailed() => Result.Failed().With(Result.Failed<int>()).ShouldFail();
        [Fact]
        void WithGenericResult_ShouldFail_IfSecondIsFailed() => Result.Succeeded().With(Result.Failed<int>()).ShouldFail();
        
        [Fact]
        void WithGenericResultFunc_ShouldSucceed_IfBothSucceeded() => Result.Succeeded().With(() => Result.Succeeded(5)).ShouldSucceed();
        [Fact]
        void WithGenericResultFunc_ShouldFail_IfFirstIsFailed() => Result.Failed().With(() => Result.Failed<int>()).ShouldFail();
        [Fact]
        void WithGenericResultFunc_ShouldFail_IfSecondIsFailed() => Result.Succeeded().With(() => Result.Failed<int>()).ShouldFail();
        
        [Fact]
        void WithResult_ShouldSucceed_IfBothSucceeded() => Result.Succeeded().With(Result.Succeeded()).ShouldSucceed();
        [Fact]
        void WithResult_ShouldFail_IfFirstIsFailed() => Result.Failed().With(Result.Failed()).ShouldFail();
        [Fact]
        void WithResult_ShouldFail_IfSecondIsFailed() => Result.Succeeded().With(Result.Failed()).ShouldFail();
        
        [Fact]
        void WithResultFunc_ShouldSucceed_IfBothSucceeded() => Result.Succeeded().With(() => Result.Succeeded()).ShouldSucceed();
        [Fact]
        void WithResultFunc_ShouldFail_IfFirstIsFailed() => Result.Failed().With(() => Result.Failed()).ShouldFail();
        [Fact]
        void WithResultFunc_ShouldFail_IfSecondIsFailed() => Result.Succeeded().With(() => Result.Failed()).ShouldFail();

        #region Helpers

        private static void Call(Result result, Func<Result, Action, Result> call)
        {
            var action = Substitute.For<Action>();
            call(result, action);
            action.Received(1).Invoke();
        }
        
        private static void NotCall(Result result, Func<Result, Action, Result> call)
        {
            var action = Substitute.For<Action>();
            call(result, action);
            action.DidNotReceive().Invoke();
        }

        #endregion
    }
}