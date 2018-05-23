using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo._04.ZeroMQ.MultipleSubscribers.PUB.SUB {

    class Program {

        static void Main(string[] args) {

            // start publisher for Area1
            Process publisherArea1Process = new Process();
            publisherArea1Process.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.04.ZeroMQ.Publisher1.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher1.PUB.exe");
            string publisherEndPointArea1a = @"tcp://*:5556";
            string publisherEndPointArea1b = @"tcp://*:5557";
            publisherArea1Process.StartInfo.Arguments = $"{publisherEndPointArea1a} {publisherEndPointArea1b}";
            publisherArea1Process.Start();
            Console.WriteLine($"started publisher Area1");
            
            // subscriber process
            Process subscriberProcess = new Process();
            subscriberProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.04.ZeroMQ.Subscriber.SUB\bin\Debug\Demo.04.ZeroMQ.Subscriber.SUB.exe");

            // start a subscriber to all Areas and all publishers usinf the defaults  
            subscriberProcess.Start();

            // start a subscriber to Area1 all publishers all messages

            // start a subscriber to Area2 all publishers all messages

            // start a subscriber to Area1a publisher all messages

            // start a subscriber to Area1b publisher all messages

            // start a subscriber to Area1a publisher only temperature messages

            // start a subscriber to Area2b publisher only temperature messages

            Console.ReadKey();
        }
    }
}
