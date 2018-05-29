using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Demo._02.ZeroMQ.Starter.PUSH.PULL {

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

            const string serverExeRelPath = @"..\..\..\Demo.02.ZeroMQ.Server.PULL\bin\Debug\Demo.02.ZeroMQ.Server.PULL.exe";
            const string clientExeRelPath = @"..\..\..\Demo.02.ZeroMQ.Client.PUSH\bin\Debug\Demo.02.ZeroMQ.Client.PUSH.exe";

            StartProcess(serverExeRelPath, null);
            Console.WriteLine("started PULL server");

            Console.WriteLine("give the server enough time to bind to its end point...");
            Thread.Sleep(1000);

            for (int i = 1; i < 5; i++) {
                StartProcess(clientExeRelPath, null);
                Console.WriteLine($"started PUSH client {i}");
            }

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
