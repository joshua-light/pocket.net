using System;

namespace Pocket.Common
{
    public struct Result
    {
        public static Result When(bool condition, string error = "") => !condition ? Failed(error) : Succeded();
        public static Result<T> Of<T>(Func<T> value) where T : class => value().AsResult();
        
        public static Result Succeded() => new Result(true);
        public static Result Failed(string error = "") => new Result(false, error);

        public static Result<T> Succeded<T>(T data) => new Result<T>(data);
        public static Result<T> Failed<T>(string error = "") => new Result<T>(error);

        private Result(bool success, string error = "")
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; }
        public bool Fail => !Success;
        
        public string Error { get; }
    }

    public struct Result<T>
    {
        private readonly T _value;

        internal Result(T value)
        {
            _value = value;
            
            Success = true;
            Error = "";
        }

        internal Result(string error)
        {
            _value = default;
            
            Success = false;
            Error = error;
        }

        public T Value
        {
            get
            {
                if (Success) return _value;
                
                throw new InvalidOperationException("Result is failed. Message: \"" + Error + "\".");
            }
        }
        
        public bool Success { get; }
        public bool Fail => !Success;
        
        public string Error { get; }
        
        #region Overloading

        public static implicit operator T(Result<T> result) => result.Value;

        public static implicit operator Result<object>(Result<T> result)
        {
            return result.Success
                ? Result.Succeded((object) result.Value)
                : Result.Failed<object>(result.Error);
        }
        
        #endregion
    }
}