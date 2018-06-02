using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo._06.Starter.QueueDevice {
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

            const string responseServerExePath = @"..\..\..\Demo.06.NetMQ.Server.Response\bin\Debug\Demo.06.NetMQ.Server.Response.exe";
            const string requestClientExePath = @"..\..\..\Demo.06.NetMQ.Client.Request\bin\Debug\Demo.06.NetMQ.Client.Request.exe";
            const string sharedQueueExePath = @"..\..\..\Demo.06.NetMQ.QueueDevice\bin\Debug\Demo.06.NetMQ.QueueDevice.exe";

            const int numberOfRequests = 5;

            // frontend proxy ROUTER socket binds and client REQ socktes connect 
            const string frontendProxyEndPoint = @"tcp://*:5678";
            const string frontendClientEndPoint = @"tcp://localhost:5678";

            // backend proxy DEALER socket binds and server REP sockets connect
            const string backendProxyEndPoint = @"tcp://*:5680";
            const string backendServerEndPoint = @"tcp://localhost:5680";

            // start and connect the server 
            StartProcess(responseServerExePath, backendServerEndPoint);
            Console.WriteLine($"started REP server on {backendServerEndPoint}");

            // start two connected the client
            StartProcess(requestClientExePath, $"{frontendClientEndPoint} {numberOfRequests} client-1");
            Console.WriteLine($"started REQ client-1 on {frontendClientEndPoint}");

            StartProcess(requestClientExePath, $"{frontendClientEndPoint} {numberOfRequests} client-2");
            Console.WriteLine($"started REQ client-2 on {frontendClientEndPoint}");

            // start the shared queue device
            StartProcess(sharedQueueExePath, $"{frontendProxyEndPoint} {backendProxyEndPoint}");
            Console.WriteLine($"started shared queue device from {frontendProxyEndPoint} to {backendProxyEndPoint}");


            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
