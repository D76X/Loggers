using System;
using System.Text;

namespace LogXtreme.Extensions {

    public static class ExceptionExtensions {

        public static string FullMessage(this Exception ex) {

            var sb = new StringBuilder();

            while(ex != null) {
                sb.Append($"{ex.Message}{Environment.NewLine}");
                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }

    public class MyClass {

        public MyClass() {
            var e = new Exception();
            e.FullMessage();
        }
    }
}
