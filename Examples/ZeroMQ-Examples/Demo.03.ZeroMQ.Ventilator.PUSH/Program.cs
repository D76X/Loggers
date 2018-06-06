using System;
using ZeroMQ;

namespace Demo._03.ZeroMQ.Ventilator.PUSH {
    class Program {
        static void Main(string[] args) {

            // The ventilator produces tasks that can be done in parallel.
            // The ventilator uses two sockets in its implementation.
            // It binds a PUSH socket to send messages to any available worker device nodes.
            // It connects a PUSH socket to send a start message to the sink.
            // It load balances the work amongst all connected workers.

            // ZMQ Context and client socket
            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket sender = context.CreateSocket(SocketType.PUSH))
            using (ZmqSocket sink = context.CreateSocket(SocketType.PUSH)) {

                // the sender socket is a stable endpoint thus it binds to an IP address.
                sender.Bind("tcp://*:5557");
                Console.WriteLine("Ventilator on tcp://*:5557");
                Console.WriteLine();

                // connect to the sink socket
                sink.Connect("tcp://localhost:5558");

                Console.WriteLine("Sever Load Balancing...");
                Console.WriteLine("Wait a bit before sending out any work.");
                Console.WriteLine("Allow time to connect to the sink.");
                Console.WriteLine("Allow tile to let the available workers to establish a connection.");
                Console.WriteLine();

                Console.WriteLine("The server is ready to send!");
                Console.ReadKey();
                //Thread.Sleep(2000);

                Console.WriteLine("send a start message to the sink...");
                var startMessage = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                sink.Send(startMessage, startMessage.Length, SocketFlags.DontWait);

                Console.WriteLine("split the work, create a batch...");
                int batchSize = 100; 
                var rnd = new Random();
                int[] workLoad = new int[batchSize];
                long totalWorkLoad = 0;

                for (int i = 0; i < batchSize; i++) {
                    workLoad[i] = rnd.Next(101);
                    totalWorkLoad += workLoad[i];
                }
                
                var totalWorkLoadMessage = BitConverter.GetBytes(totalWorkLoad);
                sink.Send(totalWorkLoadMessage, totalWorkLoadMessage.Length, SocketFlags.DontWait);
                Console.WriteLine($"sent the total workload message for this batch to the sink {BitConverter.ToString(totalWorkLoadMessage)} = {totalWorkLoad}");
                Console.WriteLine();
                
                var batchSizeMessage = BitConverter.GetBytes(batchSize);
                sink.Send(batchSizeMessage, batchSizeMessage.Length, SocketFlags.DontWait);
                Console.WriteLine($"sent the size of the batch to the sink {BitConverter.ToString(batchSizeMessage)} = {batchSize}");
                Console.WriteLine();

                // hand out the work
                for (int i = 0; i < batchSize; i++) {    
                    
                    byte[] work = BitConverter.GetBytes(workLoad[i]);
                    sender.Send(work, work.Length, SocketFlags.DontWait);
                    Console.WriteLine($"sent work {workLoad[i]}");
                }

                Console.WriteLine($"sent all work {totalWorkLoad}");
                Console.ReadKey();
            }
        }
    }
}
