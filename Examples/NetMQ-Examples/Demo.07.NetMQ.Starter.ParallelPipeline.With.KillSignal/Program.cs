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
            
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomainProcessExit);

            const string ventilatorExeRelPath = @"..\..\..\Demo.07.NetMQ.Ventilator.PushSocket\bin\Debug\Demo.07.NetMQ.Ventilator.PushSocket.exe";
            const string streamerExeRelPath = @"..\..\..\Demo.07.NetMQ.StreamerDevice\bin\Debug\Demo.07.NetMQ.StreamerDevice.exe";
            const string workerExeRelPath = @"..\..\..\Demo.07.NetMQ.Worker.Pull.Push.Sub.KillSignal\bin\Debug\Demo.07.NetMQ.Worker.Pull.Push.Sub.KillSignal.exe";
            const string sinkExeRelPath = @"..\..\..\Demo.07.NetMQ.Sink.PullSocket.With.KillSignal\bin\Debug\Demo.07.NetMQ.Sink.PullSocket.With.KillSignal.exe";

            const string streamerFrontendEndPoint = @"tcp://*:5678";
            const string streamerBackendEndPoint = @"tcp://*:5680";

            const string ventilatorEndPoint = @"tcp://localhost:5678";
            const int ventilatorBatchSize = 20;

            const string workerKillSignalEndPoint = @"tcp://localhost:5600";
            const string workerUpstreamEndPoint = @"tcp://localhost:5680";
            const string workerDownstreamEndPoint = "tcp://localhost:5682";

            const string sinkKillSignalEndPoint = @"tcp://*:5600";
            const string sinkEndPoint = "tcp://*:5682";

            int numberOfWorkers = 1;

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

            // start the sink
            string sinkArgs = $"{sinkEndPoint} {sinkKillSignalEndPoint}";
            StartProcess(sinkExeRelPath, sinkArgs);
            Console.WriteLine($"started sink with {sinkArgs}");

            // start the ventilator for job1
            string job1Name = "job1";
            string ventilatorArgs = $"{ventilatorEndPoint} {ventilatorBatchSize} {job1Name}";
            StartProcess(ventilatorExeRelPath, ventilatorArgs);
            Console.WriteLine($"started ventilator with {ventilatorArgs}");

            string workerArgs = $"{workerUpstreamEndPoint} {workerDownstreamEndPoint} {workerKillSignalEndPoint}";
            Console.WriteLine($"started {numberOfWorkers} workers");

            // start some workers
            for (int i = 1; i <= numberOfWorkers; i++) {
                StartProcess(workerExeRelPath, workerArgs);
                Console.WriteLine($"started worker {i}");
            }

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();
        }        

        private static void CurrentDomainProcessExit(object sender, EventArgs e) {

            var processes = Process.GetProcesses().Where(p => pids.Contains(p.Id));
            processes.ToList().ForEach(p => p.CloseMainWindow());
        }
    }
}
