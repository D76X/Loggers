using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;

namespace Demo._03.ZeroMQ.Worker.Device.PULL.PUSH {
    class Program {

        // The worker node uses a ZEROMQ device which comprises two socktes.
        // The worker connects to the ventilator upstream and the sink downstream.
        // The upstream connection is called the frontend.
        // The downstream connection is called the backend.
        // One PULL socket is connected to receive the messages with which work is handed out by the ventilator.
        // One PULL socket is connected to send messages to the sink with a message that notifies about the work done.

        static void Main(string[] args) {

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket receiver = context.CreateSocket(SocketType.PUSH))
            using (ZmqSocket sink = context.CreateSocket(SocketType.PUSH)) {

                // connect upstream to the ventialtor to receive the tasks.
                receiver.Connect("tcp://localhost:5557");

                // connect downstream to the sink.
                sink.Connect("tcp://localhost:5558");

                // reads messages from the ventialtor and do the work.
                while (true) {

                    // in this case the message encodes some workload as integer.
                    // however the message is a byte array. 
                    var message = new byte[4];

                    receiver.Receive(message, SocketFlags.DontWait);

                    // decode the message as info about teh work to do.
                    int workload = BitConverter.ToInt32(message, 0);

                    // do the work.
                    // in this implementation the work is done ina blocking fashion
                    // thus the worker cannot read any messages before the completion
                    // of thw work related to the last message received from the 
                    // ventialtor.
                    Thread.Sleep(workload);

                    // the work is done - notify the sink downstream.
                    sink.Send(new byte[0], 0, 0); //????
                }
            }
        }
    }
}
