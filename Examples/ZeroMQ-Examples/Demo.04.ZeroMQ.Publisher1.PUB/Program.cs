using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Publisher1.PUB {
    class Program {
        static void Main(string[] args) {

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket publisherArea1a = context.CreateSocket(SocketType.PUB))
            using (ZmqSocket publisherArea1b = context.CreateSocket(SocketType.PUB)) {

                string endPointArea1a = "tcp://*:5556";
                publisherArea1a.Bind(endPointArea1a);
                Console.WriteLine("Publisher for Area 1a bound on to {0}", endPointArea1a);

                string endPoitArea1b = "tcp://*:5557";
                publisherArea1b.Bind(endPoitArea1b);
                Console.WriteLine("Publisher for Area 1b bound on to {0}", endPoitArea1b);
                Console.WriteLine();

                var rnd = new Random();
                Frame keyArea1b = new Frame(Encoding.UTF8.GetBytes("READINGS_AREA_1B"));

                while (true) {

                    // In some cases individial messages are sent out by the PUB.
                    // The SUB end point can still subscribe to only those messages that are of interest to 
                    // them by specifying the prefix at the time of subscription to the publisher.
                    // The messages are filtered out at the PUB socket end which keeps the network traffic 
                    // to a minimum.
                    // The Send method is way of sending a whole message as a single encoded string.
                    // The Sned method is asynchronous.
                    var stArea1a = DateTime.Now;
                    var tArea1a = rnd.Next(301);
                    var pArea1a = rnd.Next(11);
                    publisherArea1a.Send($"SAMPLE_TIME_AREA_1A {stArea1a}", Encoding.Unicode);
                    publisherArea1a.Send($"TEMPERATURE_AREA_1A {tArea1a}", Encoding.Unicode);
                    publisherArea1a.Send($"PRESSURE_AREA_1A {pArea1a}", Encoding.Unicode);
                    Console.WriteLine($"Area 1A readings t = {tArea1a}, p = {pArea1a} @ {stArea1a}");
                    //Console.WriteLine();    
                   
                    // Messages can also be Multipart that is composed by more than one frame.
                    // In PUB-SUB patterns it is typical to keep the key and the data split in
                    // different frames. The first frame of a multipart message can be used as
                    // a subscription key by the subscriber.
                    // The key frame is also known as the Message envelope.
                    var stArea1b = DateTime.Now;
                    var tArea1b = rnd.Next(301);
                    var pArea1b = rnd.Next(11);
                    var message = new ZmqMessage();               
                    message.Append(keyArea1b);
                    message.Append(new Frame(BitConverter.GetBytes(stArea1b.Ticks)));
                    message.Append(new Frame(BitConverter.GetBytes(tArea1b)));
                    message.Append(new Frame(BitConverter.GetBytes(pArea1b)));
                    publisherArea1a.SendMessage(message);
                    Console.WriteLine($"Area 1B readings t = {tArea1a}, p = {pArea1a} @ {stArea1a}");

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
