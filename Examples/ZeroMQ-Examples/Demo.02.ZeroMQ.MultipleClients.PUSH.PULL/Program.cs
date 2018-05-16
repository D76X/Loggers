using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Demo._02.ZeroMQ.MultipleClients.PUSH.PULL {
    class Program {
        static void Main(string[] args) {

            Process serverProcess = new Process();
            serverProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.02.ZeroMQ.Server.PULL\bin\Debug\Demo.02.ZeroMQ.Server.PULL.exe");
            serverProcess.Start();

            Thread.Sleep(1000);

            Process clientProcess = new Process();
            clientProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.02.ZeroMQ.Client.PUSH\bin\Debug\Demo.02.ZeroMQ.Client.PUSH.exe");

            clientProcess.Start();
            clientProcess.Start();
            clientProcess.Start();
            clientProcess.Start();
        }
    }
}
