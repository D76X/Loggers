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

            var context = ZmqContext.Create();
            var socket = context.CreateSocket(SocketType.PUSH);
            socket.Connect("tcp://localhost:5555");

            Console.WriteLine("NET Sender: Started");

            //send 1000 messages and time how long that takes
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i=0; i<1000; i++)
            {
                socket.Send("Message: " + i, Encoding.UTF8);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
