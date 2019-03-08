using System;
using System.Text;

namespace Pocket.Common
{
    /// <summary>
    ///     Represents extension-methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Checks whether <paramref name="self"/> is <code>null</code> or empty string.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <returns><code>true</code> if <paramref name="self"/> is <code>null</code> or empty string, otherwise — <code>false</code>.</returns>
        public static bool IsNullOrEmpty(this string self) => 
            string.IsNullOrEmpty(self);

        /// <summary>
        ///     Returns <paramref name="self"/> if it isn't <code>null</code>, otherwise — <paramref name="@default"/>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="default">Default value that will be used instead of <paramref name="self"/> if one is null.</param>
        public static string Or(this string self, string @default) =>
            self ?? @default;

        public static int AsInt(this string self) =>
            self.As(int.Parse);
        public static int AsInt(this string self, int or) =>
            int.TryParse(self, out var result) ? result : or;
        
        public static long AsLong(this string self) =>
            self.As(long.Parse);
        public static long AsLong(this string self, long or) =>
            long.TryParse(self, out var result) ? result : or;
        
        public static string Map(this string self, Func<char, char> map)
        {
            var text = new StringBuilder();

            foreach (var ch in self)
                text.Append(map(ch));
            
            return text.ToString();
        }
        
        public static string Map(this string self, Func<char, string> map)
        {
            var text = new StringBuilder();

            foreach (var ch in self)
                text.Append(map(ch));
            
            return text.ToString();
        }

        #region Without

        public struct WithoutExpression
        {
            private readonly string _source;
            private readonly string _part;

            public WithoutExpression(string source, string part)
            {
                _source = source;
                _part = part;
            }

            public string AtEnd =>
                _source.EndsWith(_part) ? _source.Remove(_source.Length - _part.Length) : _source;
            public string AtStart =>
                _source.StartsWith(_part) ? _source.Substring(_part.Length) : _source;
        }

        public static WithoutExpression Without(this string self, string part) =>
            new WithoutExpression(self, part);

        #endregion
    }
}