using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Demo._03.ZeroMQ.Ventilator.MultipleWorkers {
    class Program {
        static void Main(string[] args) {

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
            workerProcess.Start();            

            //Process clientProcess = new Process();
            //clientProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.01.ZeroMQ.Client.REQ\bin\Debug\Demo.01.ZeroMQ.Client.REQ.exe");

            //clientProcess.Start();
            //clientProcess.Start();
            //clientProcess.Start();
            //clientProcess.Start();
        }
    }
}
