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
            using (ZmqSocket publisher_1_1 = context.CreateSocket(SocketType.PUB))
            using (ZmqSocket publisher_1_2 = context.CreateSocket(SocketType.PUB)) {

                string address_1_1 = "tcp://*:5556";
                publisher_1_1.Bind(address_1_1);
                Console.WriteLine("Publisher_1_1 bound on to {0}", address_1_1);

                string address_1_2 = "tcp://*:5557";
                publisher_1_2.Bind(address_1_2);
                Console.WriteLine("Publisher_1_2 bound on to {0}", address_1_2);

                var rnd = new Random();

                while (true) {

                    // This is way of sending a whole message as a single encoded string.
                    // The first token of the message can be used by subscribers to 
                    // selctively subscribe to spevific messages. The token is negotiated 
                    // at the time the subscription is requested by the SUB socket. The
                    // PUB socket will only send those messages to the SUB socket that it
                    // has originally subscribed to. This keeps network traffic to a minimum.

                    publisher_1_1.Send($"TEMPERATURE {rnd.Next(300)}", Encoding.Unicode);

                    // multipart messages can also be composed into a frame.
                    // In PUB-SUB patterns it is typical to have the key and the data split 
                    // into different frames. The key frame is aka Message envelope.

                    var message = new ZmqMessage();
                    Frame key = new Frame(BitConverter.GetBytes())
                    message.Append()
                    publisher_1_2.SendMessage()
                }
            }
        }
    }
}
