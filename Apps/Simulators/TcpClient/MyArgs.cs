
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;

namespace SimpleTcpClient
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class MyArgs

    {
        [ArgShortcut("H"), ArgDescription("Here some help...")]
        public string Help { get; set; }


        private string helpPort;
        [ArgShortcut("HPort"), ArgDescription("Here some help for the command Port...")]
        public string HelpPort
        {
            get
            {
                return this.helpPort;
            }
            set
            { this.helpPort = "Help Port: This is the port number the socket connects to in order to send messages."; }
        }

        private string helpSample;
        [ArgShortcut("HSample"), ArgDescription("Here some help for the command Sample...")]
        public string HelpSample
        {
            get
            {
                return this.helpSample;
            }
            set
            {
                this.helpSample = "Help Sample: This is the number of samples you would like to generate.";
            }
        }

        private string helpRepeat;
        [ArgShortcut("HRepeat"), ArgDescription("Here some help for the command Repeat...")]
        public string HelpRepeat
        {
            get
            {
                return this.helpRepeat;
            }
            set
            {
                this.helpRepeat = "Help Repeat: this is the number of sets of sample you would like to generate";
            }
        }

        private string helpTimeInterval;
        [ArgDescription("Here some help for the command Time Interval...")]
        public string HelpTimeInterval
        {
            get
            {
                return this.helpTimeInterval;
            }
            set
            {
                this.helpTimeInterval = "Help Time Interval: this is the time interval in milliseconds you can set between sets of samples";
            }
        }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("port"), ArgDescription("Port number")]
        public int PortNumber { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("sample"), ArgDescription("Number of samples")]
        public int SampleNumber { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("Interval"), ArgDescription("Time interval between samples")]
        public int TimeInterval { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("Rep"), ArgShortcut("repeatNumb"), ArgDescription("Number of sample repeats")]
        public int Repeat { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("MinV"), ArgDescription("Minimum sample value")]
        public double MinValue { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("MaxV"), ArgDescription("Maximum sample value")]
        public double MaxValue { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgShortcut("Signal"), ArgDescription("Type of signal")]
        public string SignalType { get; set; }
            
            }        }      

    


