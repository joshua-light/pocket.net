using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents value that was either succeeded or failed (then it will have error explanation) to retrieve.
    /// </summary>
    public struct Result
    {
        /// <summary>
        ///     Creates either succeeded result (if <paramref name="condition"/> is <code>true</code>) or failed (using provided <paramref name="error"/> as error description.
        /// </summary>
        /// <param name="condition">Condition that will produce <see cref="Result"/> instance.</param>
        /// <param name="error">Error description for case, when <paramref name="condition"/> is false.</param>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result When(bool condition, string error = "") =>
            !condition ? Failed(error) : Succeeded();
        
        /// <summary>
        ///     Uses function to produce a value that will be converted to succeeded result (if not <code>null</code>) or failed (otherwise).
        /// </summary>
        /// <param name="value">Function that will produce a value.</param>
        /// <typeparam name="T">Type of produced value.</typeparam>
        /// <returns>Instance of <see cref="Result{T}"/>.</returns>
        public static Result<T> Of<T>(Func<T> value) where T : class => value().AsResult();
        
        /// <summary>
        ///     Creates succeded result.
        /// </summary>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result Succeeded() => new Result(true);
        
        /// <summary>
        ///     Creates failed result using provided error description.
        /// </summary>
        /// <param name="error">Description of <see cref="Result"/>'s fail.</param>
        /// <returns>Instance of <see cref="Result{T}"/>.</returns>
        public static Result Failed(string error = "") => new Result(false, error);

        /// <summary>
        ///     Creates succeded result using specified value.
        /// </summary>
        /// <param name="value">Value that succeded result will contain.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result<T> Succeeded<T>(T value) => new Result<T>(value);
        
        /// <summary>
        ///     Creates failed result of specified type using provided error description.
        /// </summary>
        /// <param name="error">Description of <see cref="Result"/>'s fail.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result<T> Failed<T>(string error = "") => new Result<T>(error);

        private Result(bool success, string error = "")
        {
            Success = success;
            Error = error;
        }

        /// <summary>
        ///     Determines whether <see cref="Result"/> is succeded.
        /// </summary>
        public bool Success { get; }
        
        /// <summary>
        ///     Determines whether <see cref="Result"/> is failed and has an error description.
        /// </summary>
        public bool Fail => !Success;
        
        /// <summary>
        ///     Description of error by which <see cref="Result"/> is treated as failed.
        /// </summary>
        public string Error { get; }
    }
    
    /// <summary>
    ///     Represents value that was either succeeded or failed (then it will have error explanation) to retrieve.
    /// </summary>
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

        /// <summary>
        ///     Internal value of <see cref="Result{T}"/> instance (which is missing if <see cref="Result{T}"/> is failed).
        /// </summary>
        /// <exception cref="InvalidOperationException">Instance of <see cref="Result{T}"/> is in failed state.</exception>
        public T Value
        {
            get
            {
                if (!Success)
                    throw new InvalidOperationException($"Result is failed. Message: \"{Error}\".");
                
                return _value;
            }
        }
        
        /// <summary>
        ///     Determines whether <see cref="Result{T}"/> is succeded and contains a value.
        /// </summary>
        public bool Success { get; }
        
        /// <summary>
        ///     Determines whether <see cref="Result{T}"/> is failed and has an error description.
        /// </summary>
        public bool Fail => !Success;
        
        /// <summary>
        ///     Description of error by which <see cref="Result{T}"/> is treated as failed.
        /// </summary>
        public string Error { get; }

        /// <summary>
        ///     Represents current instance of <see cref="Result{T}"/> as result of other type.
        /// </summary>
        /// <remarks>
        ///    If type of current <see cref="Result{T}"/> is value type and <see cref="Success"/> is <code>true</code>
        ///    then <see cref="Value"/> will be boxed.
        /// </remarks>
        /// <typeparam name="TOut">Type of value in new <see cref="Result{T}"/>.</typeparam>
        /// <returns>Instance of new <see cref="Result{T}"/>.</returns>
        public Result<TOut> As<TOut>() =>
            Success
                ? Result.Succeeded((TOut) (object) Value)
                : Result.Failed<TOut>(Error);
        
        #region Overloading

        /// <summary>
        ///     Implicitly casts instance of <see cref="Result{T}"/> to <typeparamref name="T"/> by using <see cref="Value"/> property.
        /// </summary>
        /// <param name="result">Instance of <see cref="Result{T}"/>.</param>
        /// <returns>Inner value.</returns>
        public static implicit operator T(Result<T> result) =>
            result.Value;

        /// <summary>
        ///     Implicitly casts instance of <see cref="Result{T}"/> to <code>Result{object}</code>.
        /// </summary>
        /// <param name="result">Instance of <see cref="Result{T}"/>.</param>
        /// <returns>Instance of <code>Result{object}</code>.</returns>
        public static implicit operator Result<object>(Result<T> result) =>
            result.Success
                ? Result.Succeeded((object) result.Value)
                : Result.Failed<object>(result.Error);
        
        #endregion
    }
}