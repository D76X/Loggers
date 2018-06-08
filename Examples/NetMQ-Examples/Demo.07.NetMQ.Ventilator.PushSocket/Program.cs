using NetMQ;
using NetMQ.Sockets;
using System;

namespace Demo._07.Ventilator {

    /// <summary>
    /// Refs
    /// https://github.com/zeromq/netmq/blob/master/src/NetMQ/OutgoingSocketExtensions.cs
    /// </summary>
    class Program {

        const string defaultEndPoint = "tcp://*:5678";
        const int defaultBatchSize = 100;

        static void Main(string[] args) {

            // The ventilator produces tasks that can be done in parallel.

            bool bind = true;
            string endPoint = defaultEndPoint;
            int batchSize = defaultBatchSize;
            string jobName = Guid.NewGuid().ToString();

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678 100 job1
            // tcp://*:5678 100 job1
            if (args.Length > 0) {

                endPoint = args[0];
                bind = endPoint.Contains("*");
            }

            // tcp://localhost:5678 100 job1
            // tcp://*:5678 100 job1
            if (args.Length > 1) {

                batchSize = int.Parse(args[1]);
            }

            // tcp://localhost:5678 100 job1
            // tcp://*:5678 100 job1
            if (args.Length > 2) {

                jobName = args[2];
            }

            using (PushSocket ventilator = new PushSocket()) {

                if (bind) {
                    ventilator.Bind(endPoint);
                    Console.WriteLine($"ventilator bound to {endPoint}");
                }
                else {
                    ventilator.Connect(endPoint);
                    Console.WriteLine($"ventilator connected to {endPoint}");
                }

                Console.WriteLine("split the work, create a batch...");
                var rnd = new Random();
                int[] workLoad = new int[batchSize];
                long totalWorkLoad = 0;

                for (int i = 0; i < batchSize; i++) {
                    workLoad[i] = rnd.Next(101);
                    totalWorkLoad += workLoad[i];
                }

                // make sure that all the outgoing messages can be queued
                // in a buffer ready to be sent once a connection to a
                // receiving endpoint is established.
                ventilator.Options.SendHighWatermark = batchSize + 10;

                string header = $"{endPoint} {jobName} {totalWorkLoad} {batchSize}";                

                for (int i = 0; i < batchSize; i++) {

                    string message = $"{header} {workLoad[i]}";
                    
                    // this is not blocking if the receiving endpoint is 
                    // not connected yet the messages are going to be queued
                    // in the outgoing messages buffer.
                    ventilator.TrySendFrame(message);

                    Console.WriteLine($"sent {message}");
                }

                Console.WriteLine($"sent all work {header}");
                Console.ReadKey();
            }
        }
    }
}
