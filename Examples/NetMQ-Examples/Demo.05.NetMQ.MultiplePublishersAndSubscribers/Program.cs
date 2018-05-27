using System;
using System.Diagnostics;

namespace Demo._05.NetMQ.MultiplePublishersAndSubscribers {
    class Program {
        static void Main(string[] args) {

            // It does not matter in which order publishers, subscribers and proxy 
            // are started or stopped NetMQ/ZEROMQ takes care of connecting them
            // appropropriately.

            // First we create all the publishers 
            const string publisherExePath = @"..\..\..\Demo.05.NetMQ.Publisher\bin\Debug\Demo.05.NetMQ.Publisher.exe";
            const string subscriberExePath = @"..\..\..\Demo.05.NetMQ.Subscriber\bin\Debug\Demo.05.NetMQ.Subscriber.exe";
            const string proxyExePath = @"..\..\..\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher\bin\Debug\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher.exe";

            Process publisherAll = new Process();
            publisherAll.StartInfo.FileName = publisherExePath;
            publisherAll.Start();            

            // The we create all the subscribers
            Process subscriberAll = new Process();
            subscriberAll.StartInfo.FileName = subscriberExePath;
            subscriberAll.Start();           

            // Then we start the proxy
            Process proxy = new Process();
            proxy.StartInfo.FileName = proxyExePath;
            proxy.Start();

            Console.WriteLine("press any key to tear down all processes...");
            Console.ReadKey();

            proxy.CloseMainWindow();

            publisherAll.CloseMainWindow();

            subscriberAll.CloseMainWindow();
        }
    }
}
