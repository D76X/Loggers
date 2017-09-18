using System;
using System.Text.RegularExpressions;
using LogXtreme.Extensions;


namespace RegularExpressionTester {

    class Program {

        static void Main(string[] args) {

            bool runagain = true;

            do {               

                string pattern = string.Empty;
                string subject = string.Empty;

                if(args.Length > 0) {
                    pattern = args[0].Replace("\\n", "\n");
                }

                if(args.Length > 1) {
                    subject = args[1].Replace("\\n", "\n");
                }

                if(pattern.IsBlank()) {
                    Console.WriteLine("pattern=");
                    pattern = Console.ReadLine().Replace("\\n", "\n");
                    Console.WriteLine("subject=");
                    subject = Console.ReadLine().Replace("\\n", "\n");
                }

                var regex = new Regex(pattern);
                var match = regex.Match(subject);
                while(match.Success) {
                    Console.WriteLine($"{match.Success} @{match.Index}:{match.Length}");
                    match = match.NextMatch();
                }

                Console.WriteLine("\n\re: exit");
                runagain = !Console.ReadLine().ToLower().Trim().StartsWith("e");

            } while(runagain);
        }
    }
}
