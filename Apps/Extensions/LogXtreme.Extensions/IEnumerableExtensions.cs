using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogXtreme.Extensions {

    public static class IEnumerableExtensions {

        /// <summary>      
        /// Refs
        /// https://stackoverflow.com/questions/9697332/how-can-i-override-tostring-method-for-all-ienumerableint32
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> StringifyItems<T>(this IEnumerable<T> ienumerable) =>
            from v in ienumerable select v.ToString();

        public static string Stringify<T>(
            this IEnumerable<T> ienumerable,
            string separator = StringExtentions.Empty) {
            
            var sb = new StringBuilder();

            var length = ienumerable.Count();

            for (int i = 0; i < length-1; i++) {
                sb.Append($"{ienumerable.ElementAt(i)}{separator}");
            }

            sb.Append($"{ienumerable.ElementAt(length-1)}");

            return sb.ToString();
        }            
    }
}
