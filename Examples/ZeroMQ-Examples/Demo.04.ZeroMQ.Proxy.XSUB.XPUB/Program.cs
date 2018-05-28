using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Proxy.XSUB.XPUB {
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
            using (ZmqSocket xsubSocketFrontend = context.CreateSocket(SocketType.XSUB)) {

                xsubSocketFrontend.Bind(proxyEndPointFrontend);
                xpubSocketBackend.Bind(proxyEndPointBackend);

                Console.WriteLine($"frontend XSUB bound upstream to {proxyEndPointFrontend}");
                Console.WriteLine($"backend XPUB bound downstream to {proxyEndPointBackend}");

                // how do I start the proxy?
                // ....
                // USE A POLLER ????????
                // https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Devices/Device.cs
                // https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Devices/ForwarderDevice.cs
            }

            //Console.WriteLine("proxy XSUB-XPUB up and running...");
            //Console.ReadKey();
        }
    }
}
