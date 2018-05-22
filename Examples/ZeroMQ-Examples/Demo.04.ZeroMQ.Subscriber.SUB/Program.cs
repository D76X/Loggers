using System;
using System.Linq;
using System.Text;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Subscriber.SUB {
    class Program {
        static void Main(string[] args) {

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket subscriber = context.CreateSocket(SocketType.SUB)) {

                string endPointArea1a = "tcp://localhost:5556";
                string endPointArea1b = "tcp://localhost:5557";

                subscriber.Connect(endPointArea1a);
                subscriber.Connect(endPointArea1b);
                Console.WriteLine($"Connect to Area 1A monitor {endPointArea1a}");
                Console.WriteLine($"Connect to Area 1B monitor {endPointArea1a}");
                Console.WriteLine();

                subscriber.SubscribeAll();
                Console.WriteLine("Subcribed to all messages for Area 1");
                Console.WriteLine();

                while (true) {

                    // readings from Area1a
                    // these calls are all blocking
                    var stArea1a = subscriber.Receive(Encoding.Unicode);
                    Console.WriteLine($"received sample time area 1a {stArea1a}");
                    var tArea1a = subscriber.Receive(Encoding.Unicode);
                    Console.WriteLine($"received temperature reading area 1a {tArea1a}");
                    var pArea1a = subscriber.Receive(Encoding.Unicode);
                    Console.WriteLine($"received pressure reading area 1a {stArea1a}");

                    Console.WriteLine($"Area 1A readings t = {tArea1a.Split()[1]}, p = {pArea1a.Split()[1]} @ {stArea1a.Split()[1]}");
                    Console.WriteLine();

                    // readings from Area1b
                    ZmqMessage rArea1bMsg = subscriber.ReceiveMessage();
                    int nFrames = rArea1bMsg.FrameCount;
                    Console.WriteLine($"Area1b readings 1 message with {nFrames} frames");
                    Frame[] frames = rArea1bMsg.ToArray();
                    string keyArea1b = Encoding.UTF8.GetString(frames[0].Buffer);
                    DateTime stArea1b = new DateTime(BitConverter.ToInt64(frames[1].Buffer, 0));
                    int tArea1b = BitConverter.ToInt32(frames[2].Buffer,0);
                    int pArea1b = BitConverter.ToInt32(frames[3].Buffer, 0);
                    Console.WriteLine($"Area 1B readings t = {tArea1b}, p = {pArea1b} @ {stArea1b}");

                    Console.WriteLine();
                }
            }
        }
    }
}
