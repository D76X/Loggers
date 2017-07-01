using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
namespace ZeroMQ_SUB
{
    class Program
    {
        static void Main(string[] args)
        {
            //SUBSCRIBES TO MESSAGES 

            var context = ZmqContext.Create();
            using (var socket = context.CreateSocket(SocketType.SUB))
            {
                socket.Connect("tcp://localhost:13000");
                socket.SubscribeAll();
                while (true)
                {
                    var message = socket.Receive(Encoding.UTF8);
                    Console.WriteLine(message);
                }
            }
        }
    }
}
