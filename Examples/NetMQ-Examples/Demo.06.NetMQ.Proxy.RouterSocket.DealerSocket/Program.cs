using NetMQ.Sockets;
using System;

namespace Demo._06.Router.Dealer {
    class Program {

        static void Main(string[] args) {

            const string defaultProxyEndPointFrontend = @"tcp://localhost:5678";
            const string defaultProxyEndPointBackend = @"tcp://*:5680";

            string proxyEndPointFrontend = defaultProxyEndPointFrontend;
            string proxyEndPointBackend = defaultProxyEndPointBackend;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5680 tcp://*:5680
            if (args.Length > 1) {
                proxyEndPointFrontend= args[0];
                proxyEndPointBackend = args[1];
            }

            using (var dealerSocketBackend = new DealerSocket())
            using (var routerSocketFrontend = new RouterSocket()) {

                routerSocketFrontend.Bind(proxyEndPointFrontend);
                dealerSocketBackend.Bind(proxyEndPointBackend);

                Console.WriteLine($"frontend ROUTER bound upstream to {proxyEndPointFrontend}");
                Console.WriteLine($"backend DEALER bound downstream to {proxyEndPointBackend}");

                // create a proxy between frontend and backend
                var proxy = new NetMQ.Proxy(routerSocketFrontend, dealerSocketBackend);
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
