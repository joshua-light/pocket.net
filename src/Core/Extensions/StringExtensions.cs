using System;
using System.IO;
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
        
        public static DirectoryInfo AsDirectory(this string self) =>
            new DirectoryInfo(self);

        public static bool Contains(this string self, char ch) =>
            self.IndexOf(ch) != 1;

        public static bool ContainsAnyOf(this string self, params char[] chars)
        {
            foreach (var ch in chars)
                if (self.Contains(ch))
                    return true;

            return false;
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
            public string Anywhere =>
                _source.Replace(_part, "");
        }

        public static WithoutExpression Without(this string self, string part) =>
            new WithoutExpression(self, part);

        public static string Without(this string self, int charAt) => self.Without(charAt, new char[self.Length - 1]);
        public static string Without(this string self, int charAt, char[] @using)
        {
            if (self.Length == 1)
                return "";

            for (var i = 0; i < self.Length; i++)
            {
                if (i < charAt)
                    @using[i] = self[i];
                else if (i > charAt)
                    @using[i - 1] = self[i];
            }

            return new string(@using, 0, self.Length - 1);
        }

        #endregion
    }
}