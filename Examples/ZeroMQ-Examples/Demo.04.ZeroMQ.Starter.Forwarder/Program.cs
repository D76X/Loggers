using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Demo._04.ZeroMQ.Starter.Forwarder {

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

            // subscriber process
            const string publisher1ExePath = @"..\..\..\Demo.04.ZeroMQ.Publisher1.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher1.PUB.exe";
            const string publisher2ExePath = @"..\..\..\Demo.04.ZeroMQ.Publisher2.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher2.PUB.exe";
            const string subscriberExePath = @"..\..\..\Demo.04.ZeroMQ.Subscriber.SUB\bin\Debug\Demo.04.ZeroMQ.Subscriber.SUB.exe";
            const string forwarderExePath = @"..\..\..\Demo.04.ZeroMQ.Forwarder.XSUB.XPUB\bin\Debug\Demo.04.ZeroMQ.Forwarder.XSUB.XPUB.exe";
            const string forwarderFrontendEndPointToConnectTo = @"tcp://localhost:5678";
            const string forwarderBackendEndPointToConnectTo = @"tcp://localhost:5680";            

            // |tcp://*:6001|READINGS_AREA_1A|8|
            // start publisher for Area1
            StartProcess(publisher1ExePath, $"{forwarderFrontendEndPointToConnectTo} {forwarderFrontendEndPointToConnectTo}");
            Console.WriteLine($"started publishers for Area1.");

            StartProcess(publisher2ExePath, $"{forwarderFrontendEndPointToConnectTo}");
            Console.WriteLine($"started publishers for Area2.");
            
            // starts a subscriber to all endpoints from Area1 and all messages from each endpoint.
            StartProcess(subscriberExePath, $"{forwarderBackendEndPointToConnectTo}|READINGS_AREA_1A|8|");
            Console.WriteLine($"started subscriber to all endpoints for Area1 and all messages to any endpoint.");            
                        // starts a subscriber to messages from Area2 with prefix AREA2_TEMPEREATURE.
            StartProcess(subscriberExePath, $"{forwarderBackendEndPointToConnectTo}|AREA2_TEMPEREATURE|1|");
            Console.WriteLine($"started subscriber to messages with prefix AREA2_TEMPEREATURE from Area2 endpoint.");

            // Thanks to ZEROMQ sockets publishers and the subscribers will automatically connect to the 
            // corresponding end points of the forwarder even when the forwarder is started at a later 
            // point in time.
            StartProcess(forwarderExePath, null);

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}

