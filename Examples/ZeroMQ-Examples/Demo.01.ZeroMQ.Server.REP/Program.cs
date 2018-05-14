using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Demo._01.ZeroMQ {

    class Program {

        private const string cmdConvert = @"convert";

        static void Main(string[] args) {            

            // ZMQ Context, server socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket server = context.CreateSocket(SocketType.REP)) {

                server.Bind("tcp://*:5555");
                Console.WriteLine($"server up and running on {server.LastEndpoint}");

                string reply = "";
                string clientId = "";
                string messageId = ""; 
                string command = "";
                string value = "";
                string msgTimeStamp = "";
                DateTime receivedTimeStamps;

                while (true) {

                    string message = server.Receive(Encoding.Unicode);
                    receivedTimeStamps = DateTime.Now;

                    Console.WriteLine($"processed message = {message} @ {receivedTimeStamps}");

                    // work
                    var parts = message.Split();
                    clientId = parts[0];
                    messageId = parts[1];
                    command = parts[2];
                    value = parts[3];
                    msgTimeStamp = parts[4];

                    if (command == cmdConvert) {

                        value = $"-{value}";

                        Thread.Sleep(1000);

                        reply = $"{message} {receivedTimeStamps} {DateTime.Now} {value}";
                    }
                    else {

                        reply = $"bad message = {message}";
                    }


                    // Send reply back to client                    
                    server.Send(reply, Encoding.Unicode);
                }
            }
        }
    }
}
