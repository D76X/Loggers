using System.Diagnostics;

namespace Demo._05.NetMQ.MultiplePublishersAndSubscribers {
    class Program {
        static void Main(string[] args) {

            // It does not matter in which order publishers, subscribers and proxy 
            // are started or stopped NetMQ/ZEROMQ takes care of connecting them
            // appropropriately.

            // First we create all the publishers 
            Process publisherAll = new Process();
            publisherAll.StartInfo.FileName = @"..\..\..\Demo.05.NetMQ.Publisher\bin\Debug\Demo.05.NetMQ.Publisher.exe";
            publisherAll.Start();

            // The we create all the subscribers
            Process subscriberAll = new Process();
            subscriberAll.StartInfo.FileName = @"..\..\..\Demo.05.NetMQ.Subscriber\bin\Debug\Demo.05.NetMQ.Subscriber.exe";
            subscriberAll.Start();

            // Then we start the proxy
            Process proxy = new Process();
            proxy.StartInfo.FileName = @"..\..\..\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher\bin\Debug\Demo.05.NetMQ.Proxy.XSubscriber.XPublisher.exe";
            proxy.Start();
        }
    }
}
