using System;
using System.Diagnostics;
using ZeroMQ;

namespace Demo._03.ZeroMQ.Sink.PULL {

    class Program {


        // The sink node. 
        // It binds a single PULL socket to an IP address to receive all messages.
        // It receices a start message for the batch from the ventilator node.
        // It receices a message with the total workload for the batch from the ventilator node.
        // It receices a message with the batch sixe for the batch from the ventilator node.
        // It collects the results from the worker nodes for the bacth and compiles some stats.

        static void Main(string[] args) {           

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket sink = context.CreateSocket(SocketType.PULL)) {

                // the sink socket is a stable endpoint thus it binds to its IP address.                
                sink.Bind("tcp://*:5558");

                // the sink must wait for a start signal before doing anything else.
                // the start signal comes from the ventilator once it has handed out the work to the worker nodes.
                // the call to receive frame blocks.
                Frame frame = sink.ReceiveFrame();

                // the sink receives the expected workload for this batch
                frame = sink.ReceiveFrame();

                // the sink receives the expected batch size for this batch
                frame = sink.ReceiveFrame();

                int batchSize = 100;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < batchSize; i++) {

                    sink.ReceiveFrame();

                    // do something...
                }

                stopwatch.Stop();
                Console.WriteLine($"All work done in {stopwatch.ElapsedMilliseconds}");
            }
        }
    }
}

