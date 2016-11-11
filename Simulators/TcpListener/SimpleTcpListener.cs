using CommandLine;
using CommandLine.Text;
using System;
using System.Net;
using System.Text;
using ZeroMQ;
using SimpleTcpClient;

namespace SimpleTcpListener
{
    class SimpleTcpListener
    {
     

        static void Main(string[] args)
        {
            //RECEIVING MESSAGES FROM A QUEUE

            var context = ZmqContext.Create();
            using (var serverSocket = context.CreateSocket(SocketType.PULL))
            {
                serverSocket.Bind("tcp://*:5555");
                while (true)
                {
                    var receivedMessage = serverSocket.Receive(Encoding.UTF8);
                    Console.WriteLine("Message received " + receivedMessage);
                }



            }

        }
    }
    
}
