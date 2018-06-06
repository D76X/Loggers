using NetMQ.Sockets;
using System;

namespace Demo._07.Sink.With.KillSignal {

    class Program {

        // The sink node.
        // It binds or connect to an upstream endpoint.
        // It PULLs work-done messages from the upstream endpoint. 
        // It compiles stats on the work done based on the collected messages.
        // It sends a kill message to any of the workers that were allocated to its pipeline.

        const string defaultUpstreamEndPoint = "tcp://localhost:5580";
        const string defaultKillSignalEndPoint = "tcp://localhost:5600";

        static void Main(string[] args) {

            string upstreamEndPoint = defaultUpstreamEndPoint;
            string killSignalEndPoint = defaultKillSignalEndPoint;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5680           
            if (args.Length > 0) {

                upstreamEndPoint = args[0];
            }

            // tcp://localhost:5680 tcp://*:5600
            if (args.Length > 1) {

                killSignalEndPoint = args[1];
            }

            using (PullSocket pullSocket = new PullSocket())
            using (PublisherSocket publisherSocket = new PublisherSocket()) {

                publisherSocket.Options.SendHighWatermark = 1;
                publisherSocket.Connect(killSignalEndPoint);
                Console.WriteLine($"sink connects kill signal publisher to {killSignalEndPoint}");

                pullSocket.Connect(upstreamEndPoint);
                Console.WriteLine($"sink connects upstream with pull socket to {upstreamEndPoint}");

                // the sink must wait for a start signal before doing anything else.
                // the start signal comes from the ventilator once it has handed out 
                // the work to the worker nodes.
                // the call to receive frame blocks.
                Frame frame = sink.ReceiveFrame();
                Console.WriteLine($"received frame {BitConverter.ToString(frame.Buffer)}");

                // the sink receives the expected workload for this batch
                frame = sink.ReceiveFrame();
                Console.WriteLine($"received frame {BitConverter.ToString(frame.Buffer)} = {BitConverter.ToInt64(frame.Buffer, 0)}");

                // the sink receives the expected batch size for this batch
                frame = sink.ReceiveFrame();
                int batchSize = BitConverter.ToInt32(frame.Buffer, 0);
                Console.WriteLine($"received frame {BitConverter.ToString(frame.Buffer)} = {batchSize}");

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                int totalWorkDone = 0;
                for (int i = 0; i < batchSize; i++) {

                    frame = sink.ReceiveFrame();
                    var workLoad = BitConverter.ToInt32(frame.Buffer, 0);
                    totalWorkDone += workLoad;
                    Console.WriteLine($"worker completed workload {BitConverter.ToString(frame.Buffer)} = {workLoad}");
                }

                stopwatch.Stop();
                Console.WriteLine($"All work {totalWorkDone} done in {stopwatch.ElapsedMilliseconds}");
                Console.ReadKey();
            }
        }
    }
}
