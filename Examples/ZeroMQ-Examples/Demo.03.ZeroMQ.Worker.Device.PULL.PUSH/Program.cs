using System;
using System.Threading;
using ZeroMQ;

namespace Demo._03.ZeroMQ.Worker.Device.PULL.PUSH {
    class Program {

        // The worker node is a ZEROMQ device.
        // A ZEROMQ device as at least two socktes one upstream and one dowstream.        
        // The upstream connection is called the frontend.
        // The downstream connection is called the backend.
        // Messages flow the frontend to the backend.
        // Reply may flow form the backend to teh frontend.
        // The worker device connects to the ventilator upstream and connects to the sink downstream.
        // One PULL socket is connected to receive the messages with which work is handed out by the ventilator.
        // One PULL socket is connected to send messages to the sink with a message that notifies about the work done.

        static void Main(string[] args) {

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket receiver = context.CreateSocket(SocketType.PULL))
            using (ZmqSocket sink = context.CreateSocket(SocketType.PUSH)) {

                string upstream = "tcp://localhost:5557";
                string downstream = "tcp://localhost:5558";

                // connect upstream to the ventialtor to receive the tasks.
                receiver.Connect(upstream);
                // connect downstream to the sink.
                sink.Connect(downstream);

                Console.WriteLine($"connect upstream with PULL socktet @ {upstream}");
                Console.WriteLine($"connect downstream with PUSH socktet @ {downstream}");

                // reads messages from the ventialtor and do the work.
                while (true) {

                    // in this case the message encodes some workload as integer.
                    Frame frame = receiver.ReceiveFrame();

                    // decode the message as info about the work to do.
                    int workload = BitConverter.ToInt32(frame.Buffer, 0);

                    // do the work.
                    // in this implementation the work is done in a blocking fashion
                    // thus the worker cannot read any messages before the completion
                    // of the work related to the last message received from the 
                    // ventialtor.
                    Thread.Sleep(workload);

                    Console.WriteLine($"done work {workload}");
                    var workCompletedMessage = BitConverter.GetBytes(workload);

                    // the work is done - notify the sink downstream.
                    sink.Send(workCompletedMessage, workCompletedMessage.Length, SocketFlags.DontWait);
                }
            }
        }
    }
}
