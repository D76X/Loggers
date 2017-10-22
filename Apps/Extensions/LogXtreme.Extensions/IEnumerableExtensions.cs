using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.Extensions {

    public static class IEnumerableExtensions {

        /// <summary>      
        /// Refs
        /// https://stackoverflow.com/questions/9697332/how-can-i-override-tostring-method-for-all-ienumerableint32
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> Stringify<T>(this IEnumerable<T> ienumerable) {

            return from v in ienumerable select v.ToString();
        }       
    }
}
