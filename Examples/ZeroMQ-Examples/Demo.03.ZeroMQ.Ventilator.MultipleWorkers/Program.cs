using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Demo._03.ZeroMQ.Ventilator.MultipleWorkers {
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

            const string ventilatorExeRelPath = @"..\..\..\Demo.03.ZeroMQ.Ventilator.PUSH\bin\Debug\Demo.03.ZeroMQ.Ventilator.PUSH.exe";
            const string sinkExeRelPath = @"..\..\..\Demo.03.ZeroMQ.Sink.PULL\bin\Debug\Demo.03.ZeroMQ.Sink.PULL.exe";
            const string workerExeRelPath = @"..\..\..\Demo.03.ZeroMQ.Worker.Device.PULL.PUSH\bin\Debug\Demo.03.ZeroMQ.Worker.Device.PULL.PUSH.exe";

            int numberOfWorkers = 2;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            if (args.Length > 0) {
                int.TryParse(args[0], out numberOfWorkers);                
            }

            // start the ventilator
            StartProcess(ventilatorExeRelPath, null);
            Console.WriteLine("started ventilator");

            // start the sink
            StartProcess(sinkExeRelPath, null);
            Console.WriteLine("started sink");

            // give some time for the ventilator and the sink to be up and running
            Thread.Sleep(2000);

            // start some workers
            for (int i = 1; i <= numberOfWorkers; i++) {
                //workerProcess.Start();
                StartProcess(workerExeRelPath, null);
                Console.WriteLine($"started worker {i}");
            }

            Console.WriteLine($"started {numberOfWorkers} workers");

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
