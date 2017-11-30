using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.Extensions {

    public static class StringExtentions {

        public const string SingleSpace = @" ";
        public const string Empty = @"";

        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str) {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsBlank(this string str) {
            return string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str);
        }
    }
}
