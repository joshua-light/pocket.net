using System;
using NSubstitute;
using Pocket.Common.Extensions;
using Pocket.Common.Tests.Core.Extensions;
using Xunit;

namespace Pocket.Common.Tests.Monads.Extensions
{
    public class GenericResultExtensionsTest
    {
        [Fact]
        void OnSuccess_ShouldCallAction_IfResultIsSucceeded() => Call(Result.Succeded(5), (x, y) => x.OnSuccess(_ => y()));
        [Fact]
        void OnSuccess_ShouldNotCallAction_IfResultIsFailed() => NotCall(Result.Failed<int>(), (x, y) => x.OnSuccess(_ => y()));
        
        [Fact]
        void OnFail_ShouldCallAction_IfResultIsFailed() => Call(Result.Failed<int>(), (x, y) => x.OnFail(y));
        [Fact]
        void OnFail_ShouldNotCallAction_IfResultIsSucceeded() => NotCall(Result.Succeded(5), (x, y) => x.OnFail(y));
        
        [Fact]
        void With_ShouldSucceed_IfResultIsSucceeded() => Result.Succeded(5).With(5).ShouldSucceed();
        [Fact]
        void With_ShouldFail_IfResultIsFailed() => Result.Failed<int>().With(5).ShouldFail();
        [Fact]
        void With_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeded(1).With(5));
        
        [Fact]
        void WithFunc_ShouldSucceed_IfResultIsSucceeded() => Result.Succeded(1).With(() => 5).ShouldSucceed();
        [Fact]
        void WithFunc_ShouldFail_IfResultIsFailed() => Result.Failed<int>().With(() => 5).ShouldFail();
        [Fact]
        void WithFunc_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeded(1).With(() => 5));
        
        [Fact]
        void WithFuncSelector_ShouldSucceed_IfResultIsSucceeded() => Result.Succeded(1).With(_ => 5).ShouldSucceed();
        [Fact]
        void WithFuncSelector_ShouldFail_IfResultIsFailed() => Result.Failed<int>().With(_ => 5).ShouldFail();
        [Fact]
        void WithFuncSelector_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeded(1).With(_ => 5));
        
        [Fact]
        void WithMaybe_ShouldSucceed_IfResultIsSucceeded() => Result.Succeded(1).With(5.Just()).ShouldSucceed();
        [Fact]
        void WithMaybe_ShouldFail_IfResultIsFailed() => Result.Failed<int>().With(5.Just()).ShouldFail();
        [Fact]
        void WithMaybe_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeded(1).With(5.Just()));
        
        [Fact]
        void WithMaybeFunc_ShouldSucceed_IfResultIsSucceeded() => Result.Succeded(1).With(() => 5.Just()).ShouldSucceed();
        [Fact]
        void WithMaybeFunc_ShouldFail_IfResultIsFailed() => Result.Failed<int>().With(() => 5.Just()).ShouldFail();
        [Fact]
        void WithMaybeFunc_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeded(1).With(() => 5.Just()));
        
        [Fact]
        void WithMaybeFuncSelector_ShouldSucceed_IfResultIsSucceeded() => Result.Succeded(1).With(_ => 5.Just()).ShouldSucceed();
        [Fact]
        void WithMaybeFuncSelector_ShouldFail_IfResultIsFailed() => Result.Failed<int>().With(_ => 5.Just()).ShouldFail();
        [Fact]
        void WithMaybeFuncSelector_ShouldReturnPassedValue() => Assert.Equal(5, Result.Succeded(1).With(_ => 5.Just()));
        
        [Fact]
        void WithResult_ShouldSucceed_IfBothSucceeded() => Result.Succeded(1).With(Result.Succeded()).ShouldSucceed();
        [Fact]
        void WithResult_ShouldFail_IfFirstIsFailed() => Result.Failed<int>().With(Result.Failed()).ShouldFail();
        [Fact]
        void WithResult_ShouldFail_IfSecondIsFailed() => Result.Succeded(1).With(Result.Failed()).ShouldFail();
        
        [Fact]
        void WithResultFunc_ShouldSucceed_IfBothSucceeded() => Result.Succeded(1).With(() => Result.Succeded()).ShouldSucceed();
        [Fact]
        void WithResultFunc_ShouldFail_IfFirstIsFailed() => Result.Failed<int>().With(() => Result.Failed()).ShouldFail();
        [Fact]
        void WithResultFunc_ShouldFail_IfSecondIsFailed() => Result.Succeded(1).With(() => Result.Failed()).ShouldFail();
        
        [Fact]
        void WithResultFuncSelector_ShouldSucceed_IfBothSucceeded() => Result.Succeded(1).With(_ => Result.Succeded()).ShouldSucceed();
        [Fact]
        void WithResultFuncSelector_ShouldFail_IfFirstIsFailed() => Result.Failed<int>().With(_ => Result.Failed()).ShouldFail();
        [Fact]
        void WithResultFuncSelector_ShouldFail_IfSecondIsFailed() => Result.Succeded(1).With(_ => Result.Failed()).ShouldFail();
        
        [Fact]
        void WithGenericResult_ShouldSucceed_IfBothSucceeded() => Result.Succeded(1).With(Result.Succeded(1)).ShouldSucceed();
        [Fact]
        void WithGenericResult_ShouldFail_IfFirstIsFailed() => Result.Failed<int>().With(Result.Failed<int>()).ShouldFail();
        [Fact]
        void WithGenericResult_ShouldFail_IfSecondIsFailed() => Result.Succeded(1).With(Result.Failed<int>()).ShouldFail();
        [Fact]
        void WithGenericResult_ShouldReturnPassedValue() => Assert.Equal(2, Result.Succeded(1).With(Result.Succeded(2)));
        
        [Fact]
        void WithGenericResultFunc_ShouldSucceed_IfBothSucceeded() => Result.Succeded(1).With(() => Result.Succeded()).ShouldSucceed();
        [Fact]
        void WithGenericResultFunc_ShouldFail_IfFirstIsFailed() => Result.Failed<int>().With(() => Result.Failed()).ShouldFail();
        [Fact]
        void WithGenericResultFunc_ShouldFail_IfSecondIsFailed() => Result.Succeded(1).With(() => Result.Failed()).ShouldFail();
        [Fact]
        void WithGenericResultFunc_ShouldReturnPassedValue() => Assert.Equal(2, Result.Succeded(1).With(() => Result.Succeded(2)));
        
        [Fact]
        void WithGenericResultFuncSelector_ShouldSucceed_IfBothSucceeded() => Result.Succeded(1).With(_ => Result.Succeded()).ShouldSucceed();
        [Fact]
        void WithGenericResultFuncSelector_ShouldFail_IfFirstIsFailed() => Result.Failed<int>().With(_ => Result.Failed()).ShouldFail();
        [Fact]
        void WithGenericResultFuncSelector_ShouldFail_IfSecondIsFailed() => Result.Succeded(1).With(_ => Result.Failed()).ShouldFail();
        [Fact]
        void WithGenericResultFuncSelector_ShouldReturnPassedValue() => Assert.Equal(2, Result.Succeded(1).With(_ => Result.Succeded(2)));

        [Fact]
        void Match_ShouldCallSuccess_IfResultIsSucceeded()
        {
            var success = Substitute.For<Func<string>>();
            Result.Succeded(5).Match(success, () => "error");

            success.Received(1).Invoke();
        }
        
        [Fact]
        void Match_ShouldCallFail_IfResultIsFailed()
        {
            var fail = Substitute.For<Func<string>>();
            Result.Failed<int>().Match(() => "", fail);

            fail.Received(1).Invoke();
        }
        
        [Fact]
        void MatchWithParameter_ShouldCallSuccess_IfResultIsSucceeded()
        {
            var success = Substitute.For<Func<int, string>>();
            Result.Succeded(5).Match(success, () => "error");

            success.Received(1).Invoke(5);
        }
        
        [Fact]
        void MatchWithParameter_ShouldCallFail_IfResultIsFailed()
        {
            var fail = Substitute.For<Func<string>>();
            Result.Failed<int>().Match(_ => "", fail);

            fail.Received(1).Invoke();
        }
        
        #region Helpers

        private static void Call<T>(Result<T> result, Func<Result<T>, Action, Result<T>> call)
        {
            var action = Substitute.For<Action>();
            call(result, action);
            action.Received(1).Invoke();
        }
        
        private static void NotCall<T>(Result<T> result, Func<Result<T>, Action, Result<T>> call)
        {
            var action = Substitute.For<Action>();
            call(result, action);
            action.DidNotReceive().Invoke();
        }

        #endregion
    }
}