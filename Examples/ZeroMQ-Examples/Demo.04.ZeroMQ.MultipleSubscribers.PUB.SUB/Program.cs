using System;
using System.Diagnostics;
using System.IO;

namespace Demo._04.ZeroMQ.MultipleSubscribers.PUB.SUB {

    class Program {

        static void Main(string[] args) {

            // |tcp://*:6001|READINGS_AREA_1A|8|
            // start publisher for Area1
            Process publisherArea1Process = new Process();
            publisherArea1Process.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.04.ZeroMQ.Publisher1.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher1.PUB.exe");
            string publisherEndPointArea1a = @"tcp://*:6001";
            string publisherEndPointArea1b = @"tcp://*:6002";            
            publisherArea1Process.StartInfo.Arguments = $"{publisherEndPointArea1a} {publisherEndPointArea1b}";
            publisherArea1Process.Start();
            Console.WriteLine($"started publisher Area1 on endpoints {publisherEndPointArea1a} and {publisherEndPointArea1b}.");

            // subscriber process
            string subscriberArgs = null;
            Process subscriberProcess = new Process();
            subscriberProcess.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.04.ZeroMQ.Subscriber.SUB\bin\Debug\Demo.04.ZeroMQ.Subscriber.SUB.exe");            
            
            // starts a subscriber to all endpoints from Area1 and all messages from each endpoint.  
            subscriberProcess.Start();
            Console.WriteLine($"started subscriber to all endpoints for Area1 and all messages to any endpoint.");

            // start a subscriber to Area1a publisher endpoint and messages with key READINGS_AREA_1A
            subscriberArgs = $"tcp://localhost:6001|READINGS_AREA_1A|8|";
            subscriberProcess.StartInfo.Arguments = subscriberArgs;
            subscriberProcess.Start();
            Console.WriteLine($"started subscriber to messages with prefix READINGS_AREA_1A from Are1a endpoint.");

            // start a subscriber to Area1b publisher endpoint and messages with key READINGS_AREA_1B
            subscriberArgs = $"tcp://localhost:6002|READINGS_AREA_1B|8|";
            subscriberProcess.StartInfo.Arguments = subscriberArgs;
            subscriberProcess.Start();
            Console.WriteLine($"started subscriber to messages with prefix READINGS_AREA_1B from Are1b endpoint.");

            // start publisher for Area2
            Process publisherArea2Process = new Process();
            publisherArea2Process.StartInfo.FileName = Path.GetFullPath(@"..\..\..\Demo.04.ZeroMQ.Publisher2.PUB\bin\Debug\Demo.04.ZeroMQ.Publisher2.PUB.exe");
            string publisherEndPointArea2 = @"tcp://*:7001";
            publisherArea2Process.StartInfo.Arguments = $"{publisherEndPointArea2}";
            publisherArea2Process.Start();
            Console.WriteLine($"started publisher Area2 on endpoint {publisherEndPointArea2}.");

            // start a subscriber to the only endpoint of Area1b but only for the messages with keys TEMPEREATURE and PRESSURE
            subscriberArgs = $"tcp://localhost:7001|AREA2_TEMPEREATURE+AREA2_PRESSURE|1";
            subscriberProcess.StartInfo.Arguments = subscriberArgs;
            subscriberProcess.Start();
            Console.WriteLine($"started subscriber to messages with prefix TEMPEREATURE and PRESSURE from Area2 endpoint.");

            Console.ReadKey();
        }
    }
}
