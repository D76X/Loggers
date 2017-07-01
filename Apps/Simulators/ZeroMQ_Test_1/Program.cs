using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
using System.Diagnostics;

namespace ZeroMQ_Test_1
{
    class Program
    {
        static void Main(string[] args)
        {

            //SENDING MESSAGES TO A QUEUE

            var context =  ZmqContext.Create();
            Stopwatch stopwatch = new Stopwatch();
            using (var clientSocket = context.CreateSocket(SocketType.PUSH))
            {
                clientSocket.Connect("tcp://localhost:13000");
                Console.WriteLine("NET Sender: Started");

                //send 1000 messages and time how long that takes                
                stopwatch.Start();
                for (int i = 0; i < 2000000; i++)
                {
                    clientSocket.Send("Message: " + i, Encoding.UTF8);
                }
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);                
            }
        }
    }
}
