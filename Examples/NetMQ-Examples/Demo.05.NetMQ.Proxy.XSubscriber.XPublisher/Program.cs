using System;
using NetMQ.Sockets;

namespace Demo._05.Proxy.XSubscriber.XPublisher {

    /// <summary>
    /// Refs
    /// https://github.com/zeromq/netmq/issues/121
    /// </summary>
    class Program {

        static void Main(string[] args) {

            const string defaultProxyEndPointFrontend = @"tcp://localhost:5678";

            // There exist differces between these two addresses
            // @"tcp://localhost:5680"
            // @"tcp://*:5680"
            // * -> 0.0.0.0 is an IPv4
            // localhost -> 0.0.0.0.0.1 (IPv6)
            // This might have an effect when you set the firewall rules to 
            // forward the port of the XPUB socket!
            const string defaultProxyEndPointBackend = @"tcp://*:5680";

            string proxyEndPointFrontend = defaultProxyEndPointFrontend;
            string proxyEndPointBackend = defaultProxyEndPointBackend;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5680 tcp://*:5680
            if (args.Length > 1) {
                proxyEndPointBackend = args[0];
                proxyEndPointBackend = args[1];
            }

            using (var xpubSocketBackend = new XPublisherSocket())
            using (var xsubSocketFrontend = new XSubscriberSocket()) {

                xsubSocketFrontend.Bind(proxyEndPointFrontend);
                xpubSocketBackend.Bind(proxyEndPointBackend);

                Console.WriteLine($"frontend XSUB bound upstream to {proxyEndPointFrontend}");
                Console.WriteLine($"backend XPUB bound downstream to {proxyEndPointBackend}");

                // create a proxy between frontend and backend
                var proxy = new NetMQ.Proxy(xsubSocketFrontend, xpubSocketBackend);
                Console.WriteLine($"starting proxy from {proxyEndPointFrontend} to {proxyEndPointBackend}...");

                // blocks indefinitely
                proxy.Start();

                // this message does not show on the console
                Console.WriteLine("proxy started...");
                
                // this is not necessary 
                Console.ReadKey();
            }
        }
    }
}
