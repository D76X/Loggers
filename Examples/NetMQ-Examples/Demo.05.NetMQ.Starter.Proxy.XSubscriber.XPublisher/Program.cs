using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo._05.NetMQ.Starter.Proxy.XSubscriber.XPublisher {
    class Program {

        static HashSet<int> pids = new HashSet<int>();

        static void StartProcess(string exeRelPath, string arguments) {

            Process process = new Process();
            process.StartInfo.FileName = Path.GetFullPath(exeRelPath);
            process.StartInfo.Arguments = arguments;
            process.Start();
            pids.Add(process.Id);
        }

        static void Main(string[] args) {

            // It does not matter in which order publishers, subscribers and proxy 
            // are started or stopped NetMQ/ZEROMQ takes care of connecting them
            // appropropriately.
            
            const string publisherExePath = @"..\..\..\Demo.05.NetMQ.Publisher\bin\Debug\Demo.05.NetMQ.Publisher.exe";
            const string subscriberExePath = @"..\..\..\Demo.05.NetMQ.Subscriber\bin\Debug\Demo.05.NetMQ.Subscriber.exe";
            const string proxyExePath = @"..\..\..\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher\bin\Debug\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher.exe";
            const string proxyFrontend = @"tcp://localhost:5678";
            const string proxyBackend = @"tcp://localhost:5680";

            // First we create all the publishers 
            StartProcess(publisherExePath, null);
            Console.WriteLine("started publisher topic ALL");

            StartProcess(publisherExePath, $"{proxyFrontend} 1000 TEMPERATURE");
            Console.WriteLine("started publisher for topic TEMPERATURE");

            StartProcess(publisherExePath, $"{proxyFrontend} 1000 VENTILATION");
            Console.WriteLine("started publisher for topic VENTILATION");

            // The we create all the subscribers
            StartProcess(subscriberExePath, null);
            Console.WriteLine("started subscriber to topic ALL");

            StartProcess(subscriberExePath, $"{proxyBackend} 1 TEMPERATURE");
            Console.WriteLine("started subscriber to topic TEMPERATURE");

            StartProcess(subscriberExePath, $"{proxyBackend} 1 VENTILATION");
            Console.WriteLine("started subscriber to topic VENTILATION");

            // Then we start the proxy to bridge between the publishers and the subscribers
            StartProcess(proxyExePath, null);
            Console.WriteLine("started proxy process");

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
