using System;
using System.Text;
using ZeroMQ;

namespace Demo._02.ZeroMQ.Client.PUSH {
    class Program {
        static void Main(string[] args) {

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket client = context.CreateSocket(SocketType.PUSH)) {

                client.Connect("tcp://localhost:5555");

                string clientId = Guid.NewGuid().ToString();
                string messageId = "";
                string command = "convert";
                string value = "";
                string request = "";

                for (int requestNum = 0; requestNum < 10; requestNum++) {

                    value = requestNum.ToString();
                    messageId = value;

                    request = $"{clientId} {messageId} {command} {value} {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}";

                    client.Send(request, Encoding.Unicode);

                    Console.WriteLine($"request[{requestNum}] = {request}");
                    Console.WriteLine();

                    // a PULL socket cannot receive messages 
                    // ZEROMQ thros an exeption if an attempt is made
                    //string reply = client.Receive(Encoding.Unicode);
                }
            }

            Console.WriteLine("The client has no more messages for the server.");
            Console.ReadKey();
        }
    }
}
