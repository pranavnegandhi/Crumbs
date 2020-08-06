using System;

namespace Notadesigner.Crumbs.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Extracts a substring of desired length by counting backwards from the end.
        /// If the string is shorter than the <c>length</c>, then the entire string
        /// is returned.
        /// </summary>
        /// <param name="original">The string from which the substring has to be extracted.</param>
        /// <param name="length">The maximum number of characters desired in the substring.</param>
        /// <returns>A string that has at most <c>length</c> characters, counting from the end of the original string.</returns>
        public static string TrailingSubstring(this string original, int length)
        {
            if (string.IsNullOrEmpty(original))
            {
                return string.Empty;
            }

            length = Math.Min(length, original.Length);
            length = Math.Max(length, 0);

            length = Math.Min(length, original.Length);
            var startIndex = original.Length - length;
            var result = original.Substring(startIndex, length);

            return result;
        }
    }
}