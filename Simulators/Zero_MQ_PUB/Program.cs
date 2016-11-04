using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
using System.Diagnostics;
using System.Threading;

namespace Zero_MQ_PUB
{
    class Program
    {
        static void Main(string[] args)
        {
            //SENDING MESSAGES TO A QUEUE

            var context = ZmqContext.Create();
            Stopwatch stopwatch = new Stopwatch();
            using (var clientSocket = context.CreateSocket(SocketType.PUB))
            {
                clientSocket.Bind("tcp://*:13000");
                Console.WriteLine("NET Sender: Started");

                //send 1000 messages and time how long that takes                
                stopwatch.Start();
                var i = 0;
               while(true)
                {
                    clientSocket.Send("A Message: " + i, Encoding.UTF8);
                    clientSocket.Send("B Message: " + i, Encoding.UTF8);
                    Thread.Sleep(100);
                    i++;
                    stopwatch.Stop();
                }
                
                Console.WriteLine("ZeroMQ PUB: " + stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
