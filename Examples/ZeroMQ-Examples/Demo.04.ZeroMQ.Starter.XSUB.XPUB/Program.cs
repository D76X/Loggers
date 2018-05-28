using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo._04.ZeroMQ.Starter.XSUB.XPUB {
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
            const string proxyExePath = @"..\..\..\Demo.04.ZeroMQ.Proxy.XSUB.XPUB\bin\Debug\Demo.04.ZeroMQ.Proxy.XSUB.XPUB.exe";

            // start the proxy
            StartProcess(proxyExePath, null);

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
