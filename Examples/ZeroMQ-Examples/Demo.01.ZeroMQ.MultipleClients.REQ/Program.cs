using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Demo._01.ZeroMQ.MultipleClients.REQ {
    class Program {
        static void Main(string[] args) {

            Process serverProcess = new Process();
            serverProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.01.ZeroMQ.Server.REP\bin\Debug\Demo.01.ZeroMQ.Server.REP.exe");
            serverProcess.Start();

            Thread.Sleep(1000);

            Process clientProcess = new Process();            
            clientProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.01.ZeroMQ.Client.REQ\bin\Debug\Demo.01.ZeroMQ.Client.REQ.exe");

            clientProcess.Start();
            clientProcess.Start();
            clientProcess.Start();
            clientProcess.Start();
        }
    }
}
