using System;
using NullGuard;

namespace Pocket.Common.Extensions
{
    public static class ResultExtensions
    {
        public static Result<T> AsResult<T>([AllowNull] this T self) where T : class => self.Maybe().AsResult();
        public static Result<T> AsResult<T>(this Maybe<T> self) =>
            self.IsNothing ? Result.Failed<T>("Value is nothing.") : Result.Succeded(self.Value);

        public static Result OnSuccess(this Result self, Action action) => self.On(x => x.Success, action);
        public static Result OnFail(this Result self, Action action) => self.On(x => x.Fail, action);
        private static Result On(this Result self, Func<Result, bool> predicate, Action action)
        {
            if (predicate(self)) action();
            return self;
        }
        
        public static Result<T> With<T>(this Result self, T other) => self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<T> other) =>
            self.Success ? Result.Succeded(other()) : Result.Failed<T>(self.Error);
   
        public static Result<T> With<T>(this Result self, Maybe<T> other) => self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<Maybe<T>> other) =>
            self.Success ? other().AsResult() : Result.Failed<T>(self.Error);
        
        public static Result<T> With<T>(this Result self, Result<T> other) => self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<Result<T>> other) =>
            self.Success ? other() : Result.Failed<T>(self.Error);
        
        public static Result With(this Result self, Result other) => self.With(() => other);
        public static Result With(this Result self, Func<Result> other) =>
            self.Success ? other() : self;
    }
}