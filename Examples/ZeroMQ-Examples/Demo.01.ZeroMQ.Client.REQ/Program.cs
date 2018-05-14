using System;
using System.Text;
using ZeroMQ;

namespace Demo._01.ZeroMQ.Client {

    class Program {

        static void Main(string[] args) {
            
            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket client = context.CreateSocket(SocketType.REQ)) {

                client.Connect("tcp://localhost:5555");

                string clientId = Guid.NewGuid().ToString();
                string messageId = "";
                string command = "convert";
                string value = "";
                string request = "";                

                for (int requestNum = 0; requestNum < 10; requestNum++) {

                    value = requestNum.ToString();
                    messageId = value;
                    
                    request = $"{clientId} {messageId} {command} {value} {DateTime.Now}";                 

                    client.Send(request, Encoding.Unicode);

                    Console.WriteLine($"request[{requestNum}] = {request}");
                    Console.WriteLine();

                    string reply = client.Receive(Encoding.Unicode);

                    Console.WriteLine($"reply[{requestNum}] @ {DateTime.Now} = {reply}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("The client has no morerequest for the server.");
            Console.ReadKey();
        }
    }
}
