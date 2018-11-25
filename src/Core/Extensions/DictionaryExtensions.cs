using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="IDictionary{T, K}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {      
        /// <summary>
        ///     Gets element by specified key or default value, if one doesn't exist.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="key">Key of element to get.</param>
        /// <typeparam name="TKey">Type of keys in dictionary.</typeparam>
        /// <typeparam name="TValue">Type of values in dictionary.</typeparam>
        /// <returns>Element with specified key or default value for type <typeparamref name="TValue"/>.</returns>
        public static TValue One<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key) =>
            self.TryGetValue(key, out var result) ? result : default;
        
        /// <summary>
        ///     Gets element by specified key or sets new value, if one doesn't exist.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="key">Key of element to get.</param>
        /// <param name="or">Function that creates new value.</param>
        /// <typeparam name="TKey">Type of keys in dictionary.</typeparam>
        /// <typeparam name="TValue">Type of values in dictionary.</typeparam>
        /// <returns>Element or newly created value with specified key.</returns>
        public static TValue One<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TValue> or) =>
            self.TryGetValue(key, out var result) ? result : self[key] = or();

        /// <summary>
        ///     Gets element by specified key or throws exception with more verbose message than indexer's one.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="key">Key of element to get.</param>
        /// <typeparam name="TKey">Type of keys in dictionary.</typeparam>
        /// <typeparam name="TValue">Type of values in dictionary.</typeparam>
        /// <returns>Element with specified key.</returns>
        /// <exception cref="KeyNotFoundException">Specified <paramref name="key"/> was not found.</exception>
        public static TValue GetOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key) =>
            self.TryGetValue(key, out var result)
                ? result
                : throw new KeyNotFoundException($"Couldn't find value by [ {key} ] key.");
    }
}