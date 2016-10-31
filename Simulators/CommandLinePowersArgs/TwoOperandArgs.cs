using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;


namespace CommandLinePowersArgs
{
    public class TwoOperandArgs
    {
        [ArgRequired, ArgDescription("The first operand to process"), ArgPosition(1)]
        public double Value1 { get; set; }
        [ArgRequired, ArgDescription("The second operand to process"), ArgPosition(2)]
        public double Value2 { get; set; }
    }
}
