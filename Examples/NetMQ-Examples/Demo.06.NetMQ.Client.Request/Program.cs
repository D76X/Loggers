using NetMQ;
using NetMQ.Sockets;
using System;

namespace Demo._06.Client.Request {

    class Program {

        const string defaultServerEndPoint = @"tcp://localhost:5678";

        static void Main(string[] args) {

            string serverEndPoint = defaultServerEndPoint;
            int numberOfRequests = -1;
            string clientName = "";

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678 100 name
            if (args.Length > 0) {
                serverEndPoint = args[0];
            }

            if (args.Length > 1) {
                var x = int.Parse(args[1]);
                numberOfRequests = x >= 0 ? x : numberOfRequests;
            }

            if (args.Length > 2) {
                clientName = args[2];
            }            

            using (RequestSocket requestSocket = new RequestSocket()) {

                requestSocket.Connect(serverEndPoint);
                Console.WriteLine($"REQ socket connected to {serverEndPoint}");

                bool next = true;                

                while (next) {

                    string request = $"{clientName} request @ {DateTime.Now}";

                    requestSocket.SendFrame(request);
                    Console.WriteLine($"sent : {request}");

                    // this blocks until the server sends back the reply
                    string reply = requestSocket.ReceiveFrameString();
                    Console.WriteLine($"received : {reply}");

                    if (numberOfRequests == -1) {
                        break;
                    }

                    numberOfRequests -= 1;
                    next = numberOfRequests > 0;
                }

                Console.WriteLine("Completed!");
                Console.ReadKey();
            }
        }
    }
}
