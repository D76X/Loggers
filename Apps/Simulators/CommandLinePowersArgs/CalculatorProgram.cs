using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;

namespace CommandLinePowersArgs
{    
        [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
        public class CalculatorProgram
        {
            [HelpHook, ArgShortcut("-?"), ArgDescription("Shows this help")]
            public bool Help { get; set; }

            [ArgActionMethod, ArgDescription("Adds the two operands")]
            public void Add(TwoOperandArgs args)
            {
                Console.WriteLine(args.Value1 + args.Value2);
            }

            [ArgActionMethod, ArgDescription("Subtracts the two operands")]
            public void Subtract(TwoOperandArgs args)
            {
                Console.WriteLine(args.Value1 - args.Value2);
            }

            [ArgActionMethod, ArgDescription("Multiplies the two operands")]
            public void Multiply(TwoOperandArgs args)
            {
                Console.WriteLine(args.Value1 * args.Value2);
            }

            [ArgActionMethod, ArgDescription("Divides the two operands")]
            public void Divide(TwoOperandArgs args)
            {
                Console.WriteLine(args.Value1 / args.Value2);
            }
        }

    }

