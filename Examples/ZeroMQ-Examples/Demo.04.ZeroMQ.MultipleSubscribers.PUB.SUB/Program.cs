using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Demo._04.ZeroMQ.Starter.PUB.SUB {

    class Program {

        static HashSet<Process> processes = new HashSet<Process>();

        static void StartProcess(string exeRelPath, string arguments) {

            Process process = new Process();
            processes.Add(process);
            process.StartInfo.FileName = Path.GetFullPath(exeRelPath);
            process.StartInfo.Arguments = arguments;
            process.Start();
        }

        static void Main(string[] args) {

            // subscriber process
            const string publisher1ExePath = @"..\..\..\Demo.04.ZeroMQ.Publisher1.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher1.PUB.exe";
            const string publisher2ExePath = @"..\..\..\Demo.04.ZeroMQ.Publisher2.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher2.PUB.exe";
            const string subscriberExePath = @"..\..\..\Demo.04.ZeroMQ.Subscriber.SUB\bin\Debug\Demo.04.ZeroMQ.Subscriber.SUB.exe";            

            // |tcp://*:6001|READINGS_AREA_1A|8|
            // start publisher for Area1
            string publisherEndPointArea1a = @"tcp://*:6001";
            string publisherEndPointArea1b = @"tcp://*:6002";
            StartProcess(publisher1ExePath, $"{publisherEndPointArea1a} {publisherEndPointArea1b}");
            Console.WriteLine($"started publisher Area1 on endpoints {publisherEndPointArea1a} and {publisherEndPointArea1b}.");

            // starts a subscriber to all endpoints from Area1 and all messages from each endpoint.
            StartProcess(subscriberExePath, null);
            Console.WriteLine($"started subscriber to all endpoints for Area1 and all messages to any endpoint.");

            // start a subscriber to Area1a publisher endpoint and messages with key READINGS_AREA_1A
            StartProcess(subscriberExePath, "tcp://localhost:6001|READINGS_AREA_1A|8|");
            Console.WriteLine($"started subscriber to messages with prefix READINGS_AREA_1A from Are1a endpoint.");

            // start a subscriber to Area1b publisher endpoint and messages with key READINGS_AREA_1B
            StartProcess(subscriberExePath, "tcp://localhost:6002|READINGS_AREA_1B|8|");
            Console.WriteLine($"started subscriber to messages with prefix READINGS_AREA_1B from Are1b endpoint.");

            // start publisher for Area2
            string publisherEndPointArea2 = @"tcp://*:7001";
            StartProcess(publisher2ExePath, $"{publisherEndPointArea2}");
            Console.WriteLine($"started publisher Area2 on endpoint {publisherEndPointArea2}.");

            // start a subscriber to the only endpoint of Area1b but only for the messages with keys TEMPEREATURE and PRESSURE
            StartProcess(subscriberExePath, "tcp://localhost:7001|AREA2_TEMPEREATURE+AREA2_PRESSURE|1");
            Console.WriteLine($"started subscriber to messages with prefix TEMPEREATURE and PRESSURE from Area2 endpoint.");

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
