using System;
using System.Linq;
using System.Text;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Subscriber.SUB {

    class Program {

        private const string endPointArea1a = @"tcp://localhost:5556";
        private const string endPointArea1b = @"tcp://localhost:5557";
        private const string endPointArea2a = @"tcp://localhost:6556";
        private const string endPointArea2b = @"tcp://localhost:6557";

        static void Main(string[] args) {

            bool validArguments = false;
            bool readData = true;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            if (args.Length == 4) {
                //endPointArea1a = args[0];
                //endPointArea1b = args[1];
            }

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket subscriber = context.CreateSocket(SocketType.SUB)) {

                if (validArguments) {
                    readData = false;
                }
                else {                    
                    SetupDefaultSubscriber(subscriber);
                }

                while (readData) {

                    try {
                       
                        // subscribers get confused if ...
                        ReadDataArea1a(subscriber);
                        ReadDataArea1b(subscriber);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                    }                   
                }

                Console.ReadLine();
            }
        }

        private static void SetupDefaultSubscriber(ZmqSocket subscriber) {

            // a subscriber may connect to multiple endpoints
            subscriber.Connect(endPointArea1a);
            subscriber.Connect(endPointArea1b);

            Console.WriteLine($"Connect to Area 1A monitor {endPointArea1a}");
            Console.WriteLine($"Connect to Area 1B monitor {endPointArea1b}");
            Console.WriteLine();

            subscriber.SubscribeAll();

            Console.WriteLine("Subcribed to all messages for Area1a, Area1b, Area2a, Area2b");
            Console.WriteLine();
        }

        private static void ReadDataArea1a(ZmqSocket subscriber) {

            ZmqMessage rArea1aMsg = subscriber.ReceiveMessage();

            int nFrames = rArea1aMsg.FrameCount;
            Frame[] frames = rArea1aMsg.ToArray(); 

            string keyArea1a = Encoding.UTF8.GetString(frames[0].Buffer);
            DateTime stArea1a = DateTime.FromBinary(BitConverter.ToInt64(frames[1].Buffer, 0));
            int tArea1a = BitConverter.ToInt32(frames[2].Buffer, 0);
            int pArea1a = BitConverter.ToInt32(frames[3].Buffer, 0);

            Console.WriteLine($"Area1a readings 1 message with {nFrames} frames");
            Console.WriteLine($"Area 1A readings t = {tArea1a}, p = {pArea1a} @ {stArea1a}");
            Console.WriteLine();
        }

        private static void ReadDataArea1b(ZmqSocket subscriber) {

            ZmqMessage rArea1bMsg = subscriber.ReceiveMessage();

            int nFrames = rArea1bMsg.FrameCount;
            Frame[] frames = rArea1bMsg.ToArray();

            string keyArea1b = Encoding.UTF8.GetString(frames[0].Buffer);
            DateTime stArea1b = DateTime.FromBinary(BitConverter.ToInt64(frames[1].Buffer, 0));
            int tArea1b = BitConverter.ToInt32(frames[2].Buffer, 0);
            int pArea1b = BitConverter.ToInt32(frames[3].Buffer, 0);
            //int hArea1b = BitConverter.ToInt32(frames[4].Buffer, 0);
            //bool vArea1b = BitConverter.ToBoolean(frames[5].Buffer, 0);
            //bool cArea1b = BitConverter.ToBoolean(frames[6].Buffer, 0);

            Console.WriteLine($"Area1b readings 1 message with {nFrames} frames");
            Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b} @ {stArea1b}");
            //Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b}, h = {hArea1b}, v = {vArea1b}, c = {cArea1b} @ {stArea1b}");
            Console.WriteLine();
        }

        //private static void ReadDataArea1a(ZmqSocket subscriber) {

        //    // these calls are all blocking
        //    string stArea1a = subscriber.Receive(Encoding.Unicode);
        //    string tArea1a = subscriber.Receive(Encoding.Unicode);
        //    string pArea1a = subscriber.Receive(Encoding.Unicode);

        //    // parse - strip the prefix
        //    string stArea1aValue = stArea1a.Substring(stArea1a.IndexOf(' ') + 1);
        //    string tArea1aValue = tArea1a.Substring(tArea1a.IndexOf(' ') + 1);
        //    string ptArea1aValue = pArea1a.Substring(pArea1a.IndexOf(' ') + 1);

        //    Console.WriteLine($"received sample time area 1a {stArea1a}");
        //    Console.WriteLine($"received temperature reading area 1a {tArea1a}");
        //    Console.WriteLine($"received pressure reading area 1a {stArea1a}");
        //    Console.WriteLine($"Area 1A readings t = {tArea1aValue}, p = {ptArea1aValue} @ {stArea1aValue}");
        //    Console.WriteLine();            
        //}

        //private static void ReadDataArea1b(ZmqSocket subscriber) {

        //    // these calls are all blocking
        //    string stArea1b = subscriber.Receive(Encoding.Unicode);
        //    string tArea1b = subscriber.Receive(Encoding.Unicode);
        //    string pArea1b = subscriber.Receive(Encoding.Unicode);

        //    // parse - strip the prefix
        //    string stArea1bValue = stArea1b.Substring(stArea1b.IndexOf(' ') + 1);
        //    string tArea1bValue = tArea1b.Substring(tArea1b.IndexOf(' ') + 1);
        //    string ptArea1bValue = pArea1b.Substring(pArea1b.IndexOf(' ') + 1);

        //    Console.WriteLine($"received sample time area 1a {stArea1b}");
        //    Console.WriteLine($"received temperature reading area 1a {tArea1b}");
        //    Console.WriteLine($"received pressure reading area 1a {stArea1b}");
        //    Console.WriteLine($"Area 1B readings t = {tArea1bValue}, p = {ptArea1bValue} @ {stArea1bValue}");
        //    Console.WriteLine();
        //}

        //private static void ReadDataArea1b(ZmqSocket subscriber) {

        //    ZmqMessage rArea1bMsg = subscriber.ReceiveMessage();

        //    int nFrames = rArea1bMsg.FrameCount;
        //    Frame[] frames = rArea1bMsg.ToArray();

        //    if (nFrames != frames.Length) {
        //        Console.WriteLine("The frame count does not match the length of the message");
        //        return;
        //    }

        //    if (nFrames != 4) {

        //        Console.WriteLine("Unexpeded message format:");

        //        foreach (Frame frame in rArea1bMsg) {
        //            Console.WriteLine($"discarded frame {BitConverter.ToString(frame.Buffer)}");
        //        }

        //        Console.WriteLine();
        //        return;
        //    }

        //    // message is well formed
        //    string keyArea1b = Encoding.UTF8.GetString(frames[0].Buffer);
        //    DateTime stArea1b = DateTime.FromBinary(BitConverter.ToInt64(frames[1].Buffer, 0));
        //    int tArea1b = BitConverter.ToInt32(frames[2].Buffer, 0);
        //    int pArea1b = BitConverter.ToInt32(frames[3].Buffer, 0);

        //    Console.WriteLine($"Area1b readings 1 message with {nFrames} frames");
        //    Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b} @ {stArea1b}");
        //    Console.WriteLine();
        //}

        private static void ReadDataArea2a(ZmqSocket subscriber) {
            Console.WriteLine();
        }

        private static void ReadDataArea2b(ZmqSocket subscriber) {
            Console.WriteLine();
        }
    }
}
