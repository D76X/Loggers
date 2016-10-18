using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTcpClient
{
    public class OptionsReader
    {
        [Option('h', "help", Required = false, HelpText = "Print this help")]
        public string HelpMessage { get; set; }

        [Option('p', "port", Required = true, HelpText = "Please input the port number")]
        public int ServerPort { get; set; }

        [Option('b', "buffer", Required = true, HelpText = "Buffer size")]
        public int BufferSize { get; set; }

        [Option('i', "time interval", Required = true, HelpText =" Time interval between samples")]
        public int Milliseconds { get; set; }
    }
}
