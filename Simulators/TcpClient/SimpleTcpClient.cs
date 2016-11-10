using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
using System.Diagnostics;
using System.Threading;
using PowerArgs;

namespace SimpleTcpClient
{
    class SimpleTcpClient
    {

        static void Main(string[] args)
        {
            //SENDING MESSAGES TO A QUEUE

            try
            {
                var arguments = Args.Parse<MyArgs>(args);
                Console.WriteLine("You entered string '{0}' and '{1}' and '{2}'", arguments.Help, arguments.Port, arguments.HelpPort);



                //var context = ZmqContext.Create();
                //Stopwatch stopwatch = new Stopwatch();
                //using (var clientSocket = context.CreateSocket(SocketType.PUSH))
                //{
                //    clientSocket.Connect("tcp://localhost:13000");
                //    Console.WriteLine("NET Sender: Started");

                //    //send 1000 messages and time how long that takes                
                //    stopwatch.Start();
                //    for (int i = 0; i < 2000000; i++)
                //    {
                //        clientSocket.Send("Message: " + i, Encoding.UTF8);
                //    }
                //    stopwatch.Stop();
                //    Console.WriteLine(stopwatch.ElapsedMilliseconds);


            }
            catch (ArgException e)
            {
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
            }
        }
    }
}

            

    


