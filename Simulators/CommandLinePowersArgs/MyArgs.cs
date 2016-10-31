using PowerArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CommandLinePowerArgs
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class MyArgs

    {
        [ArgRequired(PromptIfMissing = true)]
        public string StringArg { get; set; }

        // This argument is not required, but if specified must be >= 0 and <= 60
        [ArgRange(0, 60)]
        public int IntArg { get; set; }

        public void Main()
        {
            Console.WriteLine("Main method: You entered string '{0}' and int '{1}'", this.StringArg, this.IntArg);
        }
    }
}
