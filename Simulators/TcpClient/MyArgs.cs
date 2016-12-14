
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
        [ArgDescription("Here some help...")]
        public string Help { get; set; }


        private string helpPort;
        [ArgDescription("Here some help for the command Port...")]
        public string HelpPort {
            get
            {
                return this.helpPort;
            }
            set
            { this.helpPort = "Help Port: This is the port number the socket connects to in order to send messages.";}
        }

        private string helpSample;
        [ArgDescription("Here some help for the command Sample...")]       
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
        [ArgDescription("Here some help for the command Repeat...")]
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
         
        [ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Minimum sample value")]
        public int MinValue { get; set; }

        [ArgRequired(PromptIfMissing = true)]
        [ArgDescription("Maximum sample value")]
        public int MaxValue { get; set; }


    }
}


