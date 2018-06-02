using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo._06.NetMQ.Starter.REQ.RES {
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

            const string serverEndPoint = @"tcp://*:5678";
            const string clientEndPoint = @"tcp://localhost:5678";
            const int numberOfRequests = 5;

            // start and bind the server
            StartProcess(responseServerExePath, serverEndPoint);
            Console.WriteLine($"started REP server on {serverEndPoint}");

            // start two connected the client
            StartProcess(requestClientExePath, $"{clientEndPoint} {numberOfRequests} client-1");
            Console.WriteLine($"started REQ client-1 on {clientEndPoint}");

            StartProcess(requestClientExePath, $"{clientEndPoint} {numberOfRequests} client-2");
            Console.WriteLine($"started REQ client-2 on {clientEndPoint}");

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
