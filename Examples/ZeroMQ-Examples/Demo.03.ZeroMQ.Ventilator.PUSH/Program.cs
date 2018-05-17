using System;
using ZeroMQ;

namespace Demo._03.ZeroMQ.Ventilator.PUSH {
    class Program {
        static void Main(string[] args) {

            // The ventilator produces tasks that can be done in parallel.
            // The ventilator uses two sockets in its implementation.
            // It binds a PUSH socket to send a message to any available worker device node.
            // It connects a PUSH socket to send a start message to the sink.

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket sender = context.CreateSocket(SocketType.PUSH))
            using (ZmqSocket sink = context.CreateSocket(SocketType.PUSH)) {

                // the sender socket is a stable endpoint.
                // despite being a PUSH socket it can bind to an IP address.
                sender.Bind("tcp://*:5557");

                // the sink socket
                sink.Connect("tcp://localhost:5558");

                // send a start message to the sink...
                sink.Send(new byte[] { 0x00 }, 1, SocketFlags.DontWait);

                var rnd = new Random();

                long totalWorkLoad = 0;

                for (int i = 0; i < 100; i++) {

                    int workLoad = rnd.Next(101);
                    totalWorkLoad += workLoad;
                    byte[] action = BitConverter.GetBytes(workLoad);

                    sender.Send(action, action.Length, SocketFlags.DontWait);
                }

                // prepare the message for the workes 

            }
        }
    }
}
