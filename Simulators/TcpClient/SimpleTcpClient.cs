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
                string endpoint = arguments.Port.ToString();
                using (var context = ZmqContext.Create())
                using (var clientSocket = context.CreateSocket(SocketType.PUSH))
                {
                    clientSocket.Connect("tcp://localhost:" + endpoint);

                   
                        string message = "Hello";
                        Console.WriteLine("sending " + message);
                        clientSocket.Send (message , Encoding.UTF8); 
                   
                }
            }
            catch (ArgException e)
            {
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
            }
        }
    }
}

            

    


