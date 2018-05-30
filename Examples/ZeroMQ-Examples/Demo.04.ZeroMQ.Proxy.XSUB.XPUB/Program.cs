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

                // XSUB accepts all kinds of subscriptions upstream
                // without XSUB subscribing to PUB sockets on the 
                // frontend there would be no messages to send to 
                // the backend.
                // However the XSUB may be subscribed to only some 
                // messages instead of all messages.
                xsubSocketFrontend.SubscribeAll();

                // the poller will cause this event to fire as long as
                // it detects that the XSUB socket has received messages
                // from the SUB socktes on the frontend.
                xsubSocketFrontend.ReceiveReady += (s, e) => {
                    
                    // some other fancy stuff may be done here...

                    // for a XSUB-XPUB proxy just forward all messages 
                    // to the backend.
                    xsubSocketFrontend.Forward(xpubSocketBackend);
                };                

                xpubSocketBackend.ReceiveReady += (s, e) => {

                    // some other fancy stuff may be done here...

                    // for a XSUB-XPUB proxy this is a no-op                    
                };

                // the poller polls each of its sockets to know whether they
                // have received any messages and if this the case it raises
                // the ReceiveReeady event on them.
                poller.AddSockets(new[] { xsubSocketFrontend, xpubSocketBackend });

                // binds or connect the sockets to their endpoints
                xsubSocketFrontend.Bind(proxyEndPointFrontend);
                xpubSocketBackend.Bind(proxyEndPointBackend);

                Console.WriteLine($"frontend XSUB bound upstream to {proxyEndPointFrontend}");
                Console.WriteLine($"backend XPUB bound downstream to {proxyEndPointBackend}");

                // do the polling every 500 ms
                // better strategies may be implemented
                TimeSpan timeout = TimeSpan.FromMilliseconds(500);

                while (true) {
                    poller.Poll(timeout);                     
                }
            }
        }
    }
}
