using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fclp;

namespace FluentCommandLine
{
    class Program
    {
        public class AppArguments
        {
            public int RecordId { get; set; }
            public bool Silent { get; set; }
            public string NewValue { get; set; }
        }
        static void Main(string[] args)
        {

            var p = new FluentCommandLineParser<AppArguments>();

            p.Setup(arg => arg.RecordId)
             .As('r', "record") // define the short and long option name
             .Required(); // using the standard fluent Api to declare this Option as required.

            p.Setup(arg => arg.NewValue)
             .As('v', "value")
             .Required();

            p.Setup(arg => arg.Silent)
             .As('s', "silent")
             .SetDefault(false);
            var result = p.Parse(args);

            if (result.HasErrors == false)
            {
                // use the instantiated ApplicationArguments object from the Object property on the parser.
                application.Run(p.Object);
            }
        }
    }
}
