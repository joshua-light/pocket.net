using System;

namespace Pocket
{
    public static class ResultExtensions
    {
        public static T Or<T>(this Result<T> self, T @default) =>
            self.IsOk ? self.Value : @default;
    
        public static Result<T> AsResult<T>(this T self) where T : class => self.Maybe().AsResult();
        public static Result<T> AsResult<T>(this Maybe<T> self) =>
            self.IsNothing ? Result.Fail<T>("Value is nothing.") : Result.Ok(self.Value);

        public static Result OnSuccess(this Result self, Action action) => self.On(x => x.IsOk, action);
        public static Result OnFail(this Result self, Action action) => self.On(x => x.IsFail, action);
        private static Result On(this Result self, Func<Result, bool> predicate, Action action)
        {
            if (predicate(self)) action();
            return self;
        }
        
        public static Result<T> With<T>(this Result self, T other) => self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<T> other) =>
            self.IsOk ? Result.Ok(other()) : Result.Fail<T>(self.Error);
   
        public static Result<T> With<T>(this Result self, Maybe<T> other) => self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<Maybe<T>> other) =>
            self.IsOk ? other().AsResult() : Result.Fail<T>(self.Error);
        
        public static Result<T> With<T>(this Result self, Result<T> other) => self.With(() => other);
        public static Result<T> With<T>(this Result self, Func<Result<T>> other) =>
            self.IsOk ? other() : Result.Fail<T>(self.Error);
        
        public static Result With(this Result self, Result other) => self.With(() => other);
        public static Result With(this Result self, Func<Result> other) =>
            self.IsOk ? other() : self;
    }
}