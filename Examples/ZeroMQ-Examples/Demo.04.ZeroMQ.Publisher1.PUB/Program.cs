using System;
using System.Text;
using System.Threading;
using ZeroMQ;

// It's best to send messages of the same lenght on all PUB socktes 
// that may be subscribed to by the same subscriber SUB. Mixing
// messages of different length on such publisher will cause problems
// in SUB.

namespace Demo._04.ZeroMQ.Publisher1.PUB {

    class Program {

        internal class Area1aDataReader {

            private Random rnd  = new Random();

            internal int ReadTemperature() => 1; //this.rnd.Next(301);
            internal int ReadPressure() => 10; //this.rnd.Next(11);
        }

        internal class Area1bDataReader {

            private Random rnd = new Random();
            internal object[] GetSample() => new object[] { DateTime.Now, 100, 200, 300, true, false}; 
            //new object[] { DateTime.Now, rnd.Next(301), rnd.Next(11) };            
        }

        private static Frame keyArea1a = new Frame(Encoding.UTF8.GetBytes("READINGS_AREA_1A"));
        private static Frame keyArea1b = new Frame(Encoding.UTF8.GetBytes("READINGS_AREA_1B"));
        private const string defaultEndPointArea1a = @"tcp://*:10006";
        private const string defaultEndPointArea1b = @"tcp://*:10007";

        static void Main(string[] args) {

            string endPointArea1a = defaultEndPointArea1a;
            string endPointArea1b = defaultEndPointArea1b;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            if (args.Length == 2) {
                endPointArea1a = args[0];
                endPointArea1b = args[1];
            }

            Console.WriteLine();

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket publisherArea1a = context.CreateSocket(SocketType.PUB))
            using (ZmqSocket publisherArea1b = context.CreateSocket(SocketType.PUB)) {
                
                // each PUB socket binds to its own end point.

                publisherArea1a.Bind(endPointArea1a);
                Console.WriteLine("Publisher for Area 1a bound on to {0}", endPointArea1a);

                publisherArea1b.Bind(endPointArea1b);
                Console.WriteLine("Publisher for Area 1b bound on to {0}", endPointArea1b);
                Console.WriteLine();

                var dataReaderArea1a = new Area1aDataReader();
                var dataReaderArea1b = new Area1bDataReader();

                while (true) {
                    
                    SendDataArea1a(publisherArea1a, dataReaderArea1a);                    
                    SendDataArea1b(publisherArea1b, dataReaderArea1b);

                    Thread.Sleep(1000);
                }
            }
        }

        private static void SendDataArea1a(
            ZmqSocket publisherSocket,
            Area1aDataReader areaDataReader) {

            DateTime stArea1a = DateTime.Now;
            int tArea1a = areaDataReader.ReadTemperature();
            int pArea1a = areaDataReader.ReadPressure();

            var message = new ZmqMessage();
            message.Append(keyArea1a);
            message.Append(new Frame(BitConverter.GetBytes(stArea1a.Ticks)));
            message.Append(new Frame(BitConverter.GetBytes(tArea1a)));
            message.Append(new Frame(BitConverter.GetBytes(pArea1a)));       
           
            publisherSocket.SendMessage(message);
           

            Console.WriteLine($"Area 1B readings t = {tArea1a}, p = {pArea1a} @ {stArea1a}");
        }

        private static void SendDataArea1b(
            ZmqSocket publisherSocket,
            Area1bDataReader areaDataReader) {

            var sample = areaDataReader.GetSample();

            DateTime stArea1b = (DateTime)sample[0];
            int tArea1b = (int)sample[1];
            int pArea1b = (int)sample[2];
            int hArea1b = (int)sample[3];
            bool vArea1b = (bool)sample[4];
            bool cArea1b = (bool)sample[5];

            var message = new ZmqMessage();
            message.Append(keyArea1b);
            message.Append(new Frame(BitConverter.GetBytes(stArea1b.Ticks)));
            message.Append(new Frame(BitConverter.GetBytes(tArea1b)));
            message.Append(new Frame(BitConverter.GetBytes(pArea1b)));
            //message.Append(new Frame(BitConverter.GetBytes(hArea1b)));
            //message.Append(new Frame(BitConverter.GetBytes(vArea1b)));
            //message.Append(new Frame(BitConverter.GetBytes(cArea1b)));

            publisherSocket.SendMessage(message);

            Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b}, h = {hArea1b}, v = {vArea1b}, c = {cArea1b} @ {stArea1b}");
        }

        //private static void SendDataArea1a(
        //    ZmqSocket publisherSocket,
        //    Area1aDataReader areaDataReader) {

        //    // In some cases individial messages are sent out by the PUB.
        //    // The SUB end point can still subscribe to only those messages that are of interest to 
        //    // them by specifying the prefix at the time of subscription to the publisher.
        //    // The messages are filtered out at the PUB socket end which keeps the network traffic 
        //    // to a minimum.
        //    // The Send method is way of sending a whole message as a single encoded string.
        //    // The SeNd method is asynchronous.
        //    var stArea1a = DateTime.Now;
        //    var tArea1a = areaDataReader.ReadTemperature();
        //    var pArea1a = areaDataReader.ReadPressure();
        //    publisherSocket.Send($"SAMPLE_TIME_AREA_1A {stArea1a}", Encoding.Unicode);
        //    publisherSocket.Send($"TEMPERATURE_AREA_1A {tArea1a}", Encoding.Unicode);
        //    publisherSocket.Send($"PRESSURE_AREA_1A {pArea1a}", Encoding.Unicode);
        //    Console.WriteLine($"Area 1A readings t = {tArea1a}, p = {pArea1a} @ {stArea1a}");           
        //}

        //private static void SendDataArea1b(
        //    ZmqSocket publisherSocket,
        //    Area1bDataReader areaDataReader) {

        //    //var stArea1b = DateTime.Now;
        //    //var tArea1b = areaDataReader.ReadTemperature();
        //    //var pArea1b = areaDataReader.ReadPressure();

        //    var sample = areaDataReader.GetSample();
        //    DateTime stArea1b = (DateTime)sample[0];
        //    int tArea1b = (int)sample[1];
        //    int pArea1b = (int)sample[2];

        //    publisherSocket.Send($"SAMPLE_TIME_AREA_1B {stArea1b}", Encoding.Unicode);
        //    publisherSocket.Send($"TEMPERATURE_AREA_1B {tArea1b}", Encoding.Unicode);
        //    publisherSocket.Send($"PRESSURE_AREA_1B {pArea1b}", Encoding.Unicode);

        //    Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b} @ {stArea1b}");
        //}

        //private static void SendDataArea1b(
        //    ZmqSocket publisherSocket,
        //    Area1bDataReader areaDataReader) {

        //    // Messages can also be Multipart that is composed by more than one frame.
        //    // In PUB-SUB patterns it is typical to keep the key and the data split in
        //    // different frames. The first frame of a multipart message can be used as
        //    // a subscription key by the subscriber.
        //    // The key frame is also known as the Message envelope.
        //    var sample = areaDataReader.GetSample();
        //    DateTime stArea1b = (DateTime)sample[0];
        //    int tArea1b = (int)sample[1];
        //    int pArea1b = (int)sample[2];
        //    var message = new ZmqMessage();
        //    message.Append(keyArea1b);
        //    message.Append(new Frame(BitConverter.GetBytes(stArea1b.Ticks)));
        //    message.Append(new Frame(BitConverter.GetBytes(tArea1b)));
        //    message.Append(new Frame(BitConverter.GetBytes(pArea1b)));
        //    publisherSocket.SendMessage(message);
        //    Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b} @ {stArea1b}");
        //}
    }
}
