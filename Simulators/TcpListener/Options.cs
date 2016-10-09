using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTcpListener
{
    public class Options
    {
        [Option('h', "help", Required = false, HelpText ="Print this help")]
        public string HelpMessage { get; set;}

        [Option('p', "port", Required = true, HelpText = "Please input the port number")]
        public int InputPort { get; set; }

        [Option('s', "samples", Required = true, HelpText= "Please input  sample")]
        public int Samples { get; set; }

        
    }
}
