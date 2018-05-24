using System;
using System.Linq;
using System.Text;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Subscriber.SUB {

    class Program {

        private const int defaultExpectedMessageFrameCount = 8;

        private const string keyArea1a = @"READINGS_AREA_1A";
        private const string keyArea1b = @"READINGS_AREA_1B";

        private const string endPointArea1a = @"tcp://localhost:6001";
        private const string endPointArea1b = @"tcp://localhost:6002";
        private const string endPointArea2a = @"tcp://localhost:7001";

        private static int expectedMessageFrameCount = 0;

        static void Main(string[] args) {

            bool readData = true;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }           

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket subscriber = context.CreateSocket(SocketType.SUB)) {

                SetupSubscriber(subscriber, args);

                while (readData) {

                    try {
                        ReadMessage(subscriber);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                    }
                }

                Console.ReadKey();
            }
        }

        private static void SetupSubscriber(            
            ZmqSocket subscriber,
            string[] args) {

            if (args.Length == 0) {
                SetupDefaultSubscriber(subscriber);
                return;
            }

            // expect something like
            // endpoint|MESSAGE_1_PREFIX+MESSAGE_2_PREFIX|number_of_frames_in_the_messages
            // tcp://localhost:6001|READINGS_AREA_1A|8
            // tcp://localhost:6001|TEMPEREATURE+PRESSURE|4"

            string[] split = args[0].Split('|');
            string endpoint = split[0];
            string[] prefixes = split[1].Split('+');
            expectedMessageFrameCount = int.Parse(split[2]);

            subscriber.Connect(endpoint);
            Console.WriteLine($"Connect to endpoint {endpoint}");
            
            foreach (var prefix in prefixes) {
                subscriber.Subscribe(Encoding.UTF8.GetBytes(prefix));
                Console.WriteLine($"Subcribed to message prefix {prefix}");
            }
        }

        private static void SetupDefaultSubscriber(ZmqSocket subscriber) {

            expectedMessageFrameCount = defaultExpectedMessageFrameCount;

            // a subscriber may connect to multiple endpoints
            subscriber.Connect(endPointArea1a);
            subscriber.Connect(endPointArea1b);

            Console.WriteLine($"Connect to Area 1A monitor {endPointArea1a}");
            Console.WriteLine($"Connect to Area 1B monitor {endPointArea1b}");
            Console.WriteLine();

            subscriber.SubscribeAll();

            Console.WriteLine("Subcribed to all messages from Area1a");
            Console.WriteLine();
        }

        private static void ReadMessage(ZmqSocket subscriber) {

            ZmqMessage message = subscriber.ReceiveMessage();

            if (message.FrameCount != expectedMessageFrameCount) {

                throw new ApplicationException($"message with unexpected number of frames {message.FrameCount}");
            };

            string key = Encoding.UTF8.GetString(message.First);

            switch (key) {
                case keyArea1a:
                    ReadMessageArea1a(message);
                    break;
                case keyArea1b:
                    ReadMessageArea1b(message);
                    break;
                default:
                    Console.WriteLine($"WARNING unexpected message key {key}");
                    break;
            }
        }

        private static void ReadMessageArea1a(ZmqMessage message) {

            Frame[] frames = message.ToArray();

            string key = Encoding.UTF8.GetString(frames[0].Buffer);
            string ep = Encoding.UTF8.GetString(frames[1].Buffer);
            DateTime st = DateTime.FromBinary(BitConverter.ToInt64(frames[2].Buffer, 0));
            int t = BitConverter.ToInt32(frames[3].Buffer, 0);
            int p = BitConverter.ToInt32(frames[4].Buffer, 0);

            Console.WriteLine($"key = {key}, ep = {ep}, st = {st}, t = {t}, p = {p}");
        }

        private static void ReadMessageArea1b(ZmqMessage message) {

            Frame[] frames = message.ToArray();

            string key = Encoding.UTF8.GetString(frames[0].Buffer);
            string ep = Encoding.UTF8.GetString(frames[1].Buffer);
            DateTime st = DateTime.FromBinary(BitConverter.ToInt64(frames[2].Buffer, 0));
            int t = BitConverter.ToInt32(frames[3].Buffer, 0);
            int p = BitConverter.ToInt32(frames[4].Buffer, 0);
            int h = BitConverter.ToInt32(frames[5].Buffer, 0);
            bool v = BitConverter.ToBoolean(frames[6].Buffer, 0);
            bool c = BitConverter.ToBoolean(frames[7].Buffer, 0);

            Console.WriteLine($"key = {key}, ep = {ep}, st = {st}, t = {t}, p = {p}, h = {h}, v = {v}, c = {c}");
        }

        private static void ReadDataArea2a(ZmqSocket subscriber) {
            Console.WriteLine();
        }

        private static void ReadDataArea2b(ZmqSocket subscriber) {
            Console.WriteLine();
        }
    }
}
