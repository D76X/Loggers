using CommandLinePowersArgs;
using PowerArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLinePowerArgs
{
    class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
            //    args = new string[] { "-StringArg", "a test string", "-IntArg", "10" };
            //    var parsed = Args.Parse<MyArgs>(args);

            //    Console.WriteLine("You entered string '{0}' and int '{1}'", parsed.StringArg, parsed.IntArg);
            //    Console.ReadKey();
            //}
            //catch (ArgException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
            //}

          //  Args.InvokeMain<MyArgs>(args);

            //invoke the calculator
            Args.InvokeAction<CalculatorProgram>(args);

        }
    }
}
