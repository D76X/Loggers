using NetMQ;
using NetMQ.Sockets;
using System;

namespace Demo._06.Client.Request {

    class Program {

        const string defaultServerEndPoint = @"tcp://localhost:5678";

        static void Main(string[] args) {

            string serverEndPoint = defaultServerEndPoint;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678
            if (args.Length > 0) {
                serverEndPoint = args[0];
            }

            using (RequestSocket requestSocket = new RequestSocket()) {

                requestSocket.Connect(serverEndPoint);
                Console.WriteLine($"REQ socket connected to {serverEndPoint}");

                while (true) {

                    string request = $"request @ {DateTime.Now}";

                    requestSocket.SendFrame(request);
                    Console.WriteLine($"sent : {request}");

                    // this blocks until the server sends back the reply
                    string reply = requestSocket.ReceiveFrameString();
                    Console.WriteLine($"received : {reply}");
                }
            }            
        }
    }
}
