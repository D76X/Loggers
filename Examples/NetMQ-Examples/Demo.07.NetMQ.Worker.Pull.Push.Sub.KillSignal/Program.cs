using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;

namespace Demo._07.Worker.Pull.Push.Sub.KillSignal {

    class Program {

        // The worker node is a special kind of device.
        // A device has at least two socktes one upstream and one dowstream.
        // This device has one PULL socket upstream to receive workload messages.
        // This device has one PUSH saocket downstream to send completred work to.
        // This device has also an auxiliary SUB socket to receive a termination signal.

        const string defaultUpstreamEndPoint = "tcp://localhost:5578";
        const string defaultDownstreamEndPoint = "tcp://localhost:5580";
        const string defaultKillSignalEndPoint = "tcp://localhost:5600";

        static void Main(string[] args) {

            string upstreamEndPoint = defaultUpstreamEndPoint;
            string downstreamEndPoint = defaultDownstreamEndPoint;
            string killSignalEndPoint = defaultKillSignalEndPoint;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5680 tcp://localhost:5682 tcp://localhost:5600            
            if (args.Length > 0) {
                upstreamEndPoint = args[0];
            }

            if (args.Length > 1) {
                downstreamEndPoint = args[1];
            }

            if (args.Length > 2) {
                killSignalEndPoint = args[2];
            }

            using (PullSocket pullSocket = new PullSocket())
            using (PushSocket pushSocket = new PushSocket())
            using (SubscriberSocket subscriberSocket = new SubscriberSocket()) {

                subscriberSocket.Connect(killSignalEndPoint);
                Console.WriteLine($"connect subscriber to {killSignalEndPoint}"); ;
                subscriberSocket.SubscribeToAnyTopic();
                Console.WriteLine($"subcribed to all topics");

                pullSocket.Connect(upstreamEndPoint);
                pushSocket.Connect(downstreamEndPoint);

                Console.WriteLine($"connect upstream PULL socket to {upstreamEndPoint}");
                Console.WriteLine($"connect downstrem PUSH socket to {downstreamEndPoint}");

                // do the work...
                while (true) {

                    string frame = pullSocket.ReceiveFrameString();

                    // extract workload
                    int workLoad = 1;

                    // simulate doing the work
                    Thread.Sleep(workLoad);

                    Console.WriteLine("work done...");

                    // send the result of the work to the sink
                    pushSocket.SendFrame("some work done...");

                    // check whether a kill signal was received...
                    // ...
                    var killSignalReceived = subscriberSocket.ReceiveSignal();
                }
            }
        }
    }
}
