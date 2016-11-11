using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;
using SimpleTcpClient;

namespace TcpClient
{
    public class Action
    {
        [ArgReviver, ArgDescription("Generates n random samples")]
        public static Action[] Revive(MyArgs arg)
        {
            Random random = new Random();
            int[] randomArray = new int[arg.Sample];
            for (int i = 0; i < randomArray.Length; i++)
            {
                randomArray[i] = random.Next(0, 256);
            }
            foreach(int i in randomArray)
            {
                Console.WriteLine(i.ToString());
            }
            return randomArray;

    }
}
