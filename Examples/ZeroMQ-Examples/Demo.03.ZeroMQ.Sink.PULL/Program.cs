using System;
using System.Diagnostics;
using ZeroMQ;

namespace Demo._03.ZeroMQ.Sink.PULL {

    class Program {


        // The sink node collects the results form the worker nodes.
        // It also receices a start message from the ventilator node.
        // It binds a single PULL socket to an IP address to receive all messages. 

        static void Main(string[] args) {           

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket sink = context.CreateSocket(SocketType.PUSH)) {

                // the sink socket is a stable endpoint thus it binds to its IP address.                
                sink.Bind("tcp://*:5558");

                // the sink must wait for a start signal before doing anything else.
                // the start signal comes from the ventilator once it has handed out the work to the worker nodes.
                // the call to receive frame blocks.
                sink.ReceiveFrame();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < 100; i++) {

                    // blocking receive
                    sink.ReceiveFrame();

                    // do something...
                }

                stopwatch.Stop();
                Console.WriteLine($"All work done in {stopwatch.ElapsedMilliseconds}");
            }
        }
    }
}

