using System;

namespace Pocket
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
        ///     Creates succeeded result using specified value.
        /// </summary>
        /// <param name="value">Value that succeeded result will contain.</param>
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
        ///     Determines whether <see cref="Result"/> is succeeded.
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
}