using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
using System.Diagnostics;
using System.Threading;
using PowerArgs;

namespace SimpleTcpClient
{
    class SimpleTcpClient
    {

        private static Byte[] GenerateSamples(MyArgs arg)
        {
            Random random = new Random();
            byte[] randomArray = new byte[arg.Sample];
            for (byte i = 0; i < randomArray.Length; i++)
            {
                randomArray[i] = (byte)random.Next(0, 256);

            }

            foreach (byte i in randomArray)
            {
                Console.WriteLine(i.ToString());
            }

            return randomArray;
        }
        
        static void Main(string[] args)
        {
            //SENDING MESSAGES TO A QUEUE

            try
            {

                var arguments = Args.Parse<MyArgs>(args);
                string endpoint = arguments.Port.ToString();
                using (var context = ZmqContext.Create())
                {
                    using (var clientSocket = context.CreateSocket(SocketType.PUSH))
                    {
                        clientSocket.Connect("tcp://localhost:" + endpoint);
                        //string message = "Hello, you've connected to port " + endpoint.ToString();
                        //clientSocket.Send("Message sent" + message, Encoding.UTF8);

                        byte[] samplesGenerate = GenerateSamples(arguments);
                        foreach (int i in samplesGenerate)
                        {
                            clientSocket.Send("Data " + i, Encoding.UTF8);
                        }
                    }
                }
            }

            catch (ArgException e)
            {
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
            }

        }

    }
}






