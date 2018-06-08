using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Demo._07.NetMQ.Starter.ParallelPipeline.With.KillSignal {

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

            const string ventilatorExeRelPath = @"..\..\..\Demo.07.NetMQ.Ventilator.PushSocket\bin\Debug\Demo.07.NetMQ.Ventilator.PushSocket.exe";
            const string streamerExeRelPath = @"..\..\..\Demo.07.NetMQ.StreamerDevice\bin\Debug\Demo.07.NetMQ.StreamerDevice.exe";

            //const string sinkExeRelPath = @"..\..\..\Demo.03.ZeroMQ.Sink.PULL\bin\Debug\Demo.03.ZeroMQ.Sink.PULL.exe";
            //const string workerExeRelPath = @"..\..\..\Demo.03.ZeroMQ.Worker.Device.PULL.PUSH\bin\Debug\Demo.03.ZeroMQ.Worker.Device.PULL.PUSH.exe";

            const string streamerFrontendEndPoint = @"tcp://*:5678";
            const string streamerBackendEndPoint = @"tcp://*:5680";
            const string ventilatorEndPoint = @"tcp://localhost:5678";
            const int ventilatorBatchSize = 20;

            int numberOfWorkers = 2;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            if (args.Length > 0) {
                int.TryParse(args[0], out numberOfWorkers);
            }

            // start the streamer device between the ventilators and the workers
            string streamerArgs = $"{streamerFrontendEndPoint} {streamerBackendEndPoint}";
            StartProcess(streamerExeRelPath, streamerArgs);
            Console.WriteLine($"started streamer with {streamerArgs}");

            // start the ventilator for job1
            string job1Name = "job1";
            string ventilatorArgs = $"{ventilatorEndPoint} {ventilatorBatchSize} {job1Name}";
            StartProcess(ventilatorExeRelPath, ventilatorArgs);
            Console.WriteLine($"started ventilator with {ventilatorArgs}");

            //Console.WriteLine($"started {numberOfWorkers} workers");

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
