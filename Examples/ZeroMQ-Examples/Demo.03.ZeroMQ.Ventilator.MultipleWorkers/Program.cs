using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Demo._03.ZeroMQ.Ventilator.MultipleWorkers {
    class Program {
        static void Main(string[] args) {

            int numberOfWorkers = 1;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            if (args.Length > 0) {
                int.TryParse(args[0], out numberOfWorkers);                
            }

            // start the ventilator
            Process serverProcess = new Process();
            serverProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.03.ZeroMQ.Ventilator.PUSH\bin\Debug\Demo.03.ZeroMQ.Ventilator.PUSH.exe");
            serverProcess.Start();

            // start the sink
            Process sinkProcess = new Process();
            sinkProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.03.ZeroMQ.Sink.PULL\bin\Debug\Demo.03.ZeroMQ.Sink.PULL.exe");
            sinkProcess.Start();

            // give some time for the ventilator and the sink to be up and running
            Thread.Sleep(2000);

            // start some workers
            Process workerProcess = new Process();
            workerProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.03.ZeroMQ.Worker.Device.PULL.PUSH\bin\Debug\Demo.03.ZeroMQ.Worker.Device.PULL.PUSH.exe");

            for (int i = 1; i <= numberOfWorkers; i++) {
                workerProcess.Start();
                Console.WriteLine("strated worker");
            }

            Console.WriteLine($"started {numberOfWorkers}");
            Console.ReadKey();
        }
    }
}
