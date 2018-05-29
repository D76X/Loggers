using System;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Proxy.XSUB.XPUB {

    /// <summary>
    /// Refs
    /// https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Poller.cs
    /// https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Devices/Device.cs
    /// https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Devices/ForwarderDevice.cs
    /// </summary>
    class Program {

        static void Main(string[] args) {

            // both ends of the device must bind hence @"tcp://*:PORT"
            const string defaultProxyEndPointFrontend = @"tcp://*:5678";
            const string defaultProxyEndPointBackend = @"tcp://*:5680";

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            string proxyEndPointFrontend = defaultProxyEndPointFrontend;
            string proxyEndPointBackend = defaultProxyEndPointBackend;

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket xpubSocketBackend = context.CreateSocket(SocketType.XPUB))
            using (ZmqSocket xsubSocketFrontend = context.CreateSocket(SocketType.XSUB))
            using (Poller poller = new Poller()) {

                // must set up an haldler on the forntend socket to send the message to 
                // the backend. 
                xsubSocketFrontend.ReceiveReady += (sender, socketArgs) => {

                    // here something may be done to the message before 
                    // forwarding to the backend...

                    // forward the message to teh backend
                    ((ZmqSocket)sender).Forward(xpubSocketBackend);

                };

                xpubSocketBackend.ReceiveReady += (sender, socketArgs) => {

                    // here something may be done to the message before 
                    // forwarding to the backend... 
                    //socketArgs.

                    //Frame frame = xpubSocketBackend.ReceiveFrame();
                    //xpubSocketBackend.Send(frame.Buffer, frame.Buffer.Length,
                    //    SocketFlags.DontWait);
                    //Console.WriteLine(frame.Buffer);

                    xpubSocketBackend.SendMessage(xpubSocketBackend.ReceiveMessage());
                    Console.Write("*");
                };

                // the poller drives the events between the sockets 
                poller.AddSockets(new[] { xsubSocketFrontend, xpubSocketBackend });

                // binds or connect the sockets to their endpoint
                xsubSocketFrontend.Bind(proxyEndPointFrontend);
                xpubSocketBackend.Bind(proxyEndPointBackend);

                Console.WriteLine($"frontend XSUB bound upstream to {proxyEndPointFrontend}");
                Console.WriteLine($"backend XPUB bound downstream to {proxyEndPointBackend}");

                // do the polling every 500 ms
                // better strategies may be implemented
                TimeSpan timeout = TimeSpan.FromMilliseconds(500);

                while (true) {
                    poller.Poll(timeout);
                    //Console.WriteLine("forwarding");
                }
            }
        }
    }
}
