using NetMQ;
using NetMQ.Sockets;
using System;

namespace Demo._07.Ventilator {

    class Program {

        const string defaultEndPoint = "tcp://*:5678";
        const int defaultBatchSize = 100;

        static void Main(string[] args) {

            // The ventilator produces tasks that can be done in parallel.

            bool bind = true;
            string endPoint = defaultEndPoint;
            int batchSize = defaultBatchSize;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678
            // tcp://*:5678
            if (args.Length > 0) {

                endPoint = args[0];
                bind = endPoint.Contains("*");
            }

            // tcp://localhost:5678 100
            // tcp://*:5678 100
            if (args.Length > 1) {

                batchSize = int.Parse(args[1]);
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

                var batchSizeMessage = BitConverter.GetBytes(batchSize);
                sink.Send(batchSizeMessage, batchSizeMessage.Length, SocketFlags.DontWait);
                Console.WriteLine($"sent the size of the batch to the sink {BitConverter.ToString(batchSizeMessage)} = {batchSize}");
                Console.WriteLine();

                for (int i = 0; i < batchSize; i++) {

                    //byte[] work = BitConverter.GetBytes(workLoad[i]);
                    ventilator.SendFrame("some message...");
                    Console.WriteLine($"sent work {workLoad[i]}");
                }

                Console.WriteLine($"sent all work {totalWorkLoad}");
                Console.ReadKey();
            }
        }
    }
}
