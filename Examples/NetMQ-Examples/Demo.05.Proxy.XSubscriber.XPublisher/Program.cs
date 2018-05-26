using NetMQ.Sockets;
using System;

namespace Demo._05.Proxy.XSubscriber.XPublisher {
    class Program {

        static void Main(string[] args) {

            const string defaultProxyEndPointUpstream = @"tcp://localhost:5678";
            const string defaultProxyEndPointDownstream = @"tcp://localhost:5680";

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            string proxyEndPointUpstream = defaultProxyEndPointUpstream;
            string proxyEndPointDownstream = defaultProxyEndPointDownstream;

            using (var xpubSocket = new XPublisherSocket(proxyEndPointDownstream))
            using (var xsubSocket = new XSubscriberSocket(proxyEndPointUpstream)) {

                Console.WriteLine($"frontend XSUB bound upstream to {proxyEndPointUpstream}");
                Console.WriteLine($"backend XPUB bound downstream to {proxyEndPointDownstream}");
                
                // create a proxy between frontend and backend
                var proxy = new NetMQ.Proxy(xsubSocket, xpubSocket);
                Console.WriteLine($"starting proxy from {proxyEndPointUpstream} to {proxyEndPointDownstream}...");

                // blocks indefinitely
                proxy.Start();                
            }
        }
    }
}
