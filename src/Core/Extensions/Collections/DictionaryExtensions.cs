using System;
using System.Collections.Generic;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="IDictionary{T, K}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        public struct Value<TKey, TValue>
        {
            private readonly IDictionary<TKey, TValue> _dictionary;
            private readonly TKey _key;

            public Value(IDictionary<TKey, TValue> dictionary, TKey key)
            {
                _dictionary = dictionary;
                _key = key;
            }

            public TValue OrDefault() => Or(default(TValue));

            public TValue Or(TValue @default) =>
                _dictionary.TryGetValue(_key, out var value) ? value : @default;
            public TValue Or(Func<TValue> @default) =>
                _dictionary.TryGetValue(_key, out var value) ? value : @default();
            
            public TValue OrNew(TValue @default) =>
                _dictionary.TryGetValue(_key, out var value) ? value : _dictionary[_key] = @default;
            public TValue OrNew(Func<TValue> @default) =>
                _dictionary.TryGetValue(_key, out var value) ? value : _dictionary[_key] = @default();

            public TValue OrThrow() => OrThrow($"Couldn't find value with [ {_key} ] key.");
            public TValue OrThrow(string withMessage) =>
                _dictionary.TryGetValue(_key, out var value) ? value : throw new KeyNotFoundException(withMessage);
        }

        public static Value<TKey, TValue> One<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey withKey) =>
            new Value<TKey, TValue>(self, withKey);
    }
}