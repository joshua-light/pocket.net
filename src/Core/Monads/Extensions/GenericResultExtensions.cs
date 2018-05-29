using System;

namespace Pocket.Common.Extensions
{
    public static class GenericResultExtensions
    {
        public static Result<T> OnSuccess<T>(this Result<T> self, Action<T> action)
        {
            if (self.Success) action(self.Value);
            return self;
        }
        
        public static Result<T> OnFail<T>(this Result<T> self, Action action)
        {
            if (self.Fail) action();
            return self;
        }

        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, TOut other) => self.With(_ => other);
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TOut> other) => self.With(_ => other());
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TIn, TOut> other) =>
            self.Success ? Result.Succeded(other(self.Value)) : Result.Failed<TOut>(self.Error);
        
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Maybe<TOut> other) => self.With(_ => other);
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<Maybe<TOut>> other) => self.With(_ => other());
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TIn, Maybe<TOut>> other) =>
            self.Success ? other(self.Value).AsResult() : Result.Failed<TOut>(self.Error);
        
        public static Result With<T>(this Result<T> self, Result other) => self.With(_ => other);
        public static Result With<T>(this Result<T> self, Func<Result> other) => self.With(_ => other());
        public static Result With<T>(this Result<T> self, Func<T, Result> other) =>
            self.Success ? other(self.Value) : Result.Failed(self.Error);

        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Result<TOut> other) => self.With(_ => other);
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<Result<TOut>> other) => self.With(_ => other());
        public static Result<TOut> With<TIn, TOut>(this Result<TIn> self, Func<TIn, Result<TOut>> other) =>
            self.Success ? other(self.Value) : Result.Failed<TOut>(self.Error);

        public static TOut Match<T, TOut>(this Result<T> self, Func<TOut> success, Func<TOut> fail) =>
            self.Match(_ => success(), fail);
        public static TOut Match<T, TOut>(this Result<T> self, Func<T, TOut> success, Func<TOut> fail) =>
            self.Success ? success(self.Value) : fail();
    }
}