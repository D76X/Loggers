using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;

namespace Demo._06.Server.Response {

    class Program {

        const string defaultResponseSocketEndPoint = @"tcp://*:5678";

        static void Main(string[] args) {

            string responseSocketEndPoint = defaultResponseSocketEndPoint;
            bool bind = true;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678 connect
            if (args.Length > 0) {
                responseSocketEndPoint = args[0];             
            }

            if (args.Length > 1) {
                bind = false;
            }

            using (ResponseSocket responseSocket = new ResponseSocket()) {

                if (bind) {
                    responseSocket.Bind(responseSocketEndPoint);
                    Console.WriteLine($"REP socket bound to {responseSocketEndPoint}");
                }
                else {
                    responseSocket.Connect(responseSocketEndPoint);
                    Console.WriteLine($"REP socket connected to {responseSocketEndPoint}");
                }

                // serve clients as fast as possible
                while (true) {

                    string request = responseSocket.ReceiveFrameString();
                    Console.WriteLine($"received request {DateTime.Now} : {request}");

                    // some work may be done on the server side to satisfy the request
                    Thread.Sleep(1000);

                    string reply = $"request served {DateTime.Now} : {request}";
                    responseSocket.SendFrame(reply);
                    Console.WriteLine($"sent {DateTime.Now} : {reply}");
                }
            }
        }
    }
}
