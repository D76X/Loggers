using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;

namespace SimpleTcpListener
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class MyOptions
    {
        [ArgDescription("Here some help...")]
        public string Help { get; set; }

        //[ArgRequired(PromptIfMissing = false)]
        [ArgDescription("Here some help for the command Port...")]
        public string HelpPort { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Port number")]
        public int Port { get; set; }

    }
}
