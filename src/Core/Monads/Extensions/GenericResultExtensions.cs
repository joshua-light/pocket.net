using System;

namespace Pocket
{
    public static class GenericResultExtensions
    {
        public static Result<T> OnSuccess<T>(this Result<T> self, Action<T> action)
        {
            if (self.IsOk)
                action(self.Value);
            
            return self;
        }
        
        public static Result<T> OnFail<T>(this Result<T> self, Action action)
        {
            if (self.IsFail)
                action();
            
            return self;
        }
        
        public static Result<T> OnFail<T>(this Result<T> self, Action<string> action)
        {
            if (self.IsFail)
                action(self.Error);
            
            return self;
        }

        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, TOut other) => self.With(_ => other);
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TOut> other) => self.With(_ => other());
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TIn, TOut> other) =>
            self.IsOk ? Result.Ok(other(self.Value)) : Result.Fail<TOut>(self.Error);
        
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Maybe<TOut> other) => self.With(_ => other);
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<Maybe<TOut>> other) => self.With(_ => other());
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TIn, Maybe<TOut>> other) =>
            self.IsOk ? other(self.Value).AsResult() : Result.Fail<TOut>(self.Error);
        
        public static Result With<T>(this Result<T> self, Result other) => self.With(_ => other);
        public static Result With<T>(this Result<T> self, Func<Result> other) => self.With(_ => other());
        public static Result With<T>(this Result<T> self, Func<T, Result> other) =>
            self.IsOk ? other(self.Value) : Result.Fail(self.Error);

        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Result<TOut> other) => self.With(_ => other);
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<Result<TOut>> other) => self.With(_ => other());
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TIn, Result<TOut>> other) =>
            self.IsOk ? other(self.Value) : Result.Fail<TOut>(self.Error);

        public static TOut Match<T, TOut>(this Result<T> self, Func<TOut> success, Func<TOut> fail) =>
            self.Match(_ => success(), fail);
        public static TOut Match<T, TOut>(this Result<T> self, Func<T, TOut> success, Func<TOut> fail) =>
            self.IsOk ? success(self.Value) : fail();
    }
}