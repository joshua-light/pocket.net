using System;

namespace Pocket
{
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