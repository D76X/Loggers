
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
        //[ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Here some help...")]
        public string Help { get; set; }

        //[ArgRequired(PromptIfMissing = false)]
        [ArgDescription("Here some help for the command Port...")]
        private string helpPort;
        public string HelpPort {
            get
            {
                return this.helpPort;
            }
            set
            { this.helpPort = "Help Port: This is the port number the socket connects to in order to send messages.";}
        }

        [ArgDescription("Here some help for the command Sample...")]
        private string helpSample;
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


        [ArgDescription("Here some help for the command Repeat...")]
        private string helpRepeat;
        public string HelpRepeat
        {
            get
            {
                return this.helpRepeat;
            }
            set
            {
                this.helpRepeat = "Help Repeat: this is the number of sets of sample yuo would like to generate";
            }
         }

        [ArgDescription("Here some help for the command Port...")]
        private string helpTimeInterval;
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
        [ArgDescription("Port number")]
        public int Port { get; set; }
        
        [ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Number of samples")]
        public int Sample { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Time interval between sets of samples")]
        public int TimeInterval { get; set; }


        [ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Number of sample repeats")]
        public int Repeat { get; set; }
    }
}


