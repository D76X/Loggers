using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace ZeroMQ_Test_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //RECEIVING MESSAGES FROM A QUEUE

            var context = ZmqContext.Create();
            using (var serverSocket = context.CreateSocket(SocketType.PULL))
            {
                serverSocket.Bind("tcp://*:13000");
                while (true)
                {
                    var message = serverSocket.Receive(Encoding.UTF8);
                    Console.WriteLine(message);
                }
            }
        }
    }
}
