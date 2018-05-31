using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Demo05.NetMQ.Starter.Proxy.XSubscriber.XPublisher.TransportBridging {
    class Program {

        static HashSet<int> pids = new HashSet<int>();

        static void StartProcess(string exeRelPath, string arguments) {

            Process process = new Process();
            process.StartInfo.FileName = Path.GetFullPath(exeRelPath);
            process.StartInfo.Arguments = arguments;
            process.Start();
            pids.Add(process.Id);
        }

        static string GetIpAdress() {        

            var uri = @"http://checkip.dyndns.org";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {

                var html = reader.ReadToEnd();

                var externalIP = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}").
                    Matches(html)[0].ToString();

                return externalIP;
            }
        }

        static void Main(string[] args) {

            const string publisherExePath = @"..\..\..\Demo.05.NetMQ.Publisher\bin\Debug\Demo.05.NetMQ.Publisher.exe";
            const string subscriberExePath = @"..\..\..\Demo.05.NetMQ.Subscriber\bin\Debug\Demo.05.NetMQ.Subscriber.exe";
            const string proxyExePath = @"..\..\..\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher\bin\Debug\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher.exe";

            const string proxyFrontend = @"tcp://localhost:5678";
            const int backendPort = 5680;

            // There exist differces between these two addresses
            // @"tcp://localhost:5680"
            // @"tcp://*:5680"
            // * -> 0.0.0.0 is an IPv4
            // localhost -> 0.0.0.0.0.1 (IPv6)
            // This might have an effect when you set the firewall rules to 
            // forward the port of the XPUB socket!
            string proxyBackend = $"tcp://*:{backendPort}";

            // We need to determine the external IP address assigned by your ISP
            // to your of your home/office router.
            // Then we must make sure that the port number used by the XPUB socket 
            // to listen to for incoming subcriptions is open and forwarded via 
            // your router. The port must be forwarded from the gateway address i.e. 
            // 192.168.0.1. to the network IP address assigned by the router to the 
            // machine on which the executable with the listening XPUB socket is 
            // running. 
            // The port must be open for inbound and outbound traffic. 
            // The inbound traffic must be allowed so that SUB socktes can subscribe 
            // to published messages as the subscription request must flow from the 
            // backend socket XPUB to the fronten socket. 
            // The outbound traffic must be allowed to let the published messages 
            // flow out of the XPUB socket to the subscribers.
            string externalIP = GetIpAdress();
            string proxyBackendExternalIP = $"tcp://{externalIP}:{backendPort}";

            // start a publisher on localhost:5678 which pushes messages to 
            // the frontend XSUB socket of the proxy every 1000 ms for ALL
            // topics. 
            StartProcess(publisherExePath, $"{proxyFrontend} 1000 ALL");
            Console.WriteLine("started publisher topic ALL");

            // this subscriber takes a subscription to messages with topic 
            // ALL on the external IP address.
            StartProcess(subscriberExePath, $"{proxyBackendExternalIP} 8 ALL");
            Console.WriteLine("started subscriber to topic ALL");

            // Then we start the proxy to bridge between the publishers and 
            // the subscribers.
            StartProcess(proxyExePath, $"{proxyFrontend} {proxyBackend}");
            Console.WriteLine("started proxy process");

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());            
        }
    }
}
