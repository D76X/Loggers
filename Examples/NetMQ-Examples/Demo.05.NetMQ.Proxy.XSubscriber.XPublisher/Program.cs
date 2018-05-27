using System;
using NetMQ.Sockets;

namespace Demo._05.Proxy.XSubscriber.XPublisher {        

    class Program {

        static void Main(string[] args) {

            const string defaultProxyEndPointFrontend = @"tcp://localhost:5678";
            const string defaultProxyEndPointBackend = @"tcp://localhost:5680";

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            string proxyEndPointFrontend = defaultProxyEndPointFrontend;
            string proxyEndPointBackend = defaultProxyEndPointBackend;

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
