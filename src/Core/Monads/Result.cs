using System;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents value that was either succeeded or failed (then it will have error explanation) to retrieve.
    /// </summary>
    public struct Result
    {
        /// <summary>
        ///     Uses function to produce a value that will be converted to succeeded result (if not <code>null</code>) or failed (otherwise).
        /// </summary>
        /// <param name="value">Function that will produce a value.</param>
        /// <typeparam name="T">Type of produced value.</typeparam>
        /// <returns>Instance of <see cref="Result{T}"/>.</returns>
        public static Result<T> Of<T>(Func<T> value) where T : class => value().AsResult();
        
        /// <summary>
        ///     Creates either succeeded result (if <paramref name="when"/> is <code>true</code>) or failed (using provided <paramref name="because"/> as error description.
        /// </summary>
        /// <param name="when">Condition that will produce <see cref="Result"/> instance.</param>
        /// <param name="because">Error description for case, when <paramref name="when"/> is false.</param>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result Ok(bool when, string because = "") =>
            !when ? Fail(because) : Ok();
        
        /// <summary>
        ///     Creates succeeded result.
        /// </summary>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result Ok() => new Result(true);
        
        /// <summary>
        ///     Creates succeded result using specified value.
        /// </summary>
        /// <param name="value">Value that succeded result will contain.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result<T> Ok<T>(T value) => new Result<T>(value);
        
        /// <summary>
        ///     Creates failed result using provided error description.
        /// </summary>
        /// <param name="error">Description of <see cref="Result"/>'s fail.</param>
        /// <returns>Instance of <see cref="Result{T}"/>.</returns>
        public static Result Fail(string error = "") => new Result(false, error);

        /// <summary>
        ///     Creates failed result of specified type using provided error description.
        /// </summary>
        /// <param name="error">Description of <see cref="Result"/>'s fail.</param>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <returns>Instance of <see cref="Result"/>.</returns>
        public static Result<T> Fail<T>(string error = "") => new Result<T>(error);

        private Result(bool isOk, string error = "")
        {
            IsOk = isOk;
            Error = error;
        }

        /// <summary>
        ///     Determines whether <see cref="Result"/> is succeded.
        /// </summary>
        public bool IsOk { get; }
        
        /// <summary>
        ///     Determines whether <see cref="Result"/> is failed and has an error description.
        /// </summary>
        public bool IsFail => !IsOk;
        
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
            
            IsOk = true;
            Error = "";
        }

        internal Result(string error)
        {
            _value = default;
            
            IsOk = false;
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
                if (!IsOk)
                    throw new InvalidOperationException($"Result is failed. Message: \"{Error}\".");
                
                return _value;
            }
        }
        
        /// <summary>
        ///     Determines whether <see cref="Result{T}"/> is succeded and contains a value.
        /// </summary>
        public bool IsOk { get; }
        
        /// <summary>
        ///     Determines whether <see cref="Result{T}"/> is failed and has an error description.
        /// </summary>
        public bool IsFail => !IsOk;
        
        /// <summary>
        ///     Description of error by which <see cref="Result{T}"/> is treated as failed.
        /// </summary>
        public string Error { get; }

        /// <summary>
        ///     Represents current instance of <see cref="Result{T}"/> as result of other type.
        /// </summary>
        /// <remarks>
        ///    If type of current <see cref="Result{T}"/> is value type and <see cref="IsOk"/> is <code>true</code>
        ///    then <see cref="Value"/> will be boxed.
        /// </remarks>
        /// <typeparam name="TOut">Type of value in new <see cref="Result{T}"/>.</typeparam>
        /// <returns>Instance of new <see cref="Result{T}"/>.</returns>
        public Result<TOut> As<TOut>() =>
            IsOk
                ? Result.Ok((TOut) (object) Value)
                : Result.Fail<TOut>(Error);
        
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
            result.IsOk
                ? Result.Ok((object) result.Value)
                : Result.Fail<object>(result.Error);
        
        #endregion
    }
}