using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Publisher1.PUB {

    /// <summary>
    /// Illustrate the case of multiple publisher endpoints each with a 
    /// single message type delivered as a mutltipart message.
    /// </summary>
    class Program {

        internal class Area1aDataReader {

            private Random rnd = new Random();

            internal int ReadTemperature() => this.rnd.Next(301);
            internal int ReadPressure() => this.rnd.Next(11);
        }

        internal class Area1bDataReader {

            private Random rnd = new Random();
            internal object[] GetSample() =>
                new object[] { DateTime.Now, rnd.Next(301), rnd.Next(11), rnd.Next(101), true, false };
        }

        /// <summary>
        /// In practical tests of the PUB-SUB pattern in ZEROMQ with C# binding I have 
        /// found that it's best to send messages with the same number of frames from 
        /// all PUB socktes that may be subscribed to by the same subscriber SUB socket. 
        /// Mixing messages with a different number of frames causes problems on the SUB
        /// end.
        /// </summary>
        internal static class Area1MessageFactory {

            /// <summary>
            /// The number of frames that will be carried by the message.
            /// </summary>
            internal const int Area1MessageSize = 8;

            private static ZmqMessage Pad(ZmqMessage message) {

                var missingFrames = Area1MessageSize - message.FrameCount;

                if (missingFrames < 0) {
                    throw new ApplicationException("to many frames!");
                }

                for (int i = 0; i < missingFrames; i++) {
                    message.AppendEmptyFrame();
                }

                return message;
            }

            internal static ZmqMessage CreateMessage(
                string senderEndPopint,
                Area1aDataReader dataReader) {

                var message = new ZmqMessage();

                // get all the data from the data source
                DateTime stArea1a = DateTime.Now;
                int tArea1a = dataReader.ReadTemperature();
                int pArea1a = dataReader.ReadPressure();

                // append some fixed frames at the head of the message
                message.Append(new Frame(Encoding.UTF8.GetBytes(keyArea1a)));
                message.Append(new Frame(Encoding.UTF8.GetBytes(senderEndPopint)));

                // append all the required data from the source into message frames
                message.Append(new Frame(BitConverter.GetBytes(stArea1a.Ticks)));
                message.Append(new Frame(BitConverter.GetBytes(tArea1a)));
                message.Append(new Frame(BitConverter.GetBytes(pArea1a)));

                // pad to the number of frames expected for the message
                message = Pad(message);

                return message;
            }

            internal static ZmqMessage CreateMessage(
                string senderEndPopint,
                Area1bDataReader dataReader) {

                var message = new ZmqMessage();

                // get all the data from the data source
                var sample = dataReader.GetSample();
                DateTime stArea1b = (DateTime)sample[0];
                int tArea1b = (int)sample[1];
                int pArea1b = (int)sample[2];
                int hArea1b = (int)sample[3];
                bool vArea1b = (bool)sample[4];
                bool cArea1b = (bool)sample[5];

                // append some fixed frames at the head of the message
                message.Append(new Frame(Encoding.UTF8.GetBytes(keyArea1b)));
                message.Append(new Frame(Encoding.UTF8.GetBytes(senderEndPopint)));

                // append all the required data from the source into message frames
                message.Append(new Frame(BitConverter.GetBytes(stArea1b.Ticks)));
                message.Append(new Frame(BitConverter.GetBytes(tArea1b)));
                message.Append(new Frame(BitConverter.GetBytes(pArea1b)));
                message.Append(new Frame(BitConverter.GetBytes(hArea1b)));
                message.Append(new Frame(BitConverter.GetBytes(vArea1b)));
                message.Append(new Frame(BitConverter.GetBytes(cArea1b)));

                // pad to the number of frames expected for the message
                message = Pad(message);

                return message;
            }
        }

        internal static class MessageLogger {

            internal static void LogMessageDataReader1(ZmqMessage message) {

                var enumerator = message.GetEnumerator();
                enumerator.MoveNext();

                string key = Encoding.UTF8.GetString(enumerator.Current);
                enumerator.MoveNext();

                string ep = Encoding.UTF8.GetString(enumerator.Current);
                enumerator.MoveNext();

                DateTime st = DateTime.FromBinary(BitConverter.ToInt64(enumerator.Current, 0)); ;
                enumerator.MoveNext();

                int t = BitConverter.ToInt32(enumerator.Current, 0);
                enumerator.MoveNext();

                int p = BitConverter.ToInt32(enumerator.Current, 0);

                Console.WriteLine($"key = {key}, ep = {ep}, st = {st}, t = {t}, p = {p}");
            }

            internal static void LogMessageDataReader2(ZmqMessage message) {

                var enumerator = message.GetEnumerator();
                enumerator.MoveNext();

                string key = Encoding.UTF8.GetString(enumerator.Current);
                enumerator.MoveNext();

                string ep = Encoding.UTF8.GetString(enumerator.Current);
                enumerator.MoveNext();

                DateTime st = DateTime.FromBinary(BitConverter.ToInt64(enumerator.Current, 0)); ;
                enumerator.MoveNext();

                int t = BitConverter.ToInt32(enumerator.Current, 0);
                enumerator.MoveNext();

                int p = BitConverter.ToInt32(enumerator.Current, 0);
                enumerator.MoveNext();

                int h = BitConverter.ToInt32(enumerator.Current, 0);
                enumerator.MoveNext();

                bool v = BitConverter.ToBoolean(enumerator.Current, 0);
                enumerator.MoveNext();

                bool c = BitConverter.ToBoolean(enumerator.Current, 0);

                Console.WriteLine($"key = {key}, ep = {ep}, st = {st}, t = {t}, p = {p}, h = {h}, v = {v}, c = {c}");
            }

            internal static void Log(ZmqMessage message) {

                string key = Encoding.UTF8.GetString(message.First);

                switch (key) {
                    case keyArea1a:
                        LogMessageDataReader1(message);
                        break;
                    case keyArea1b:
                        LogMessageDataReader2(message);
                        break;
                    default:
                        Console.WriteLine($"WARNING unexpected message key {key}");
                        break;
                }
            }
        }

        private const string keyArea1a = @"READINGS_AREA_1A";
        private const string keyArea1b = @"READINGS_AREA_1B";
        private const string defaultEndPointArea1a = @"tcp://*:10001";
        private const string defaultEndPointArea1b = @"tcp://*:10002";

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

            Console.WriteLine("press P to pause publishing");
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
                bool pause = false;

                while (true) {

                    if (!pause) {

                        SendDataArea1a(publisherArea1a, dataReaderArea1a);
                        SendDataArea1b(publisherArea1b, dataReaderArea1b);
                    }

                    if (Console.KeyAvailable) {

                        if (!pause && Console.ReadKey(true).Key == ConsoleKey.P) {
                            pause = true;
                            Console.WriteLine("publishing has been paused press any key but P to continue...");
                        } else {
                            pause = false;
                        }
                    }
                    
                    Thread.Sleep(1000);
                }
            }
        }

        private static void SendDataArea1a(
            ZmqSocket publisherSocket,
            Area1aDataReader areaDataReader) {

            ZmqMessage message = Area1MessageFactory.CreateMessage(
                publisherSocket.LastEndpoint,
                areaDataReader);

            publisherSocket.SendMessage(message);

            MessageLogger.Log(message);
        }

        private static void SendDataArea1b(
            ZmqSocket publisherSocket,
            Area1bDataReader areaDataReader) {

            ZmqMessage message = Area1MessageFactory.CreateMessage(
                publisherSocket.LastEndpoint,
                areaDataReader);

            publisherSocket.SendMessage(message);

            MessageLogger.Log(message);
        }
    }
}
