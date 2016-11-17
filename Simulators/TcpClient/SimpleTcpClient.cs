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

        private static void GeneralOptions( MyArgs arg)
        {
            Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
        }

        
        static void Main(string[] args)
        {
            //SENDING MESSAGES TO A QUEUE
           
            try
            {
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
                var arguments = Args.Parse<MyArgs>(args);                
                string endpoint = arguments.Port.ToString();
                using (var context = ZmqContext.Create())
                {
                    using (var clientSocket = context.CreateSocket(SocketType.PUSH))
                    {
                        clientSocket.Connect("tcp://localhost:" + endpoint);

                        if (arguments.HelpPort !=null | arguments.HelpSample != null
                            | arguments.HelpRepeat != null | arguments.HelpTimeInterval != null)
                        {
                            Console.WriteLine(arguments.HelpPort);
                        
                            Console.WriteLine(arguments.HelpSample);
                        
                            Console.WriteLine(arguments.HelpRepeat);
                      
                            Console.WriteLine(arguments.HelpTimeInterval);
                        }

                        string message = "Message sent: Hello, you've connected to port " + endpoint.ToString();
                        clientSocket.Send(message, Encoding.UTF8);
                        Console.WriteLine(message);


                        string message2 = "Number of samples generated are " + arguments.Sample + " with a time interval of " + arguments.TimeInterval + "milliseconds ";
                        clientSocket.Send(message2, Encoding.UTF8);
                        Console.WriteLine(message2);

                        for (int t = 0; t < arguments.Repeat; t ++ )
                        {
                            byte[] samplesGenerate = GenerateSamples(arguments);

                            foreach (int i in samplesGenerate)
                            {
                                clientSocket.Send(" Sent Data " + i, Encoding.UTF8);
                            }
                            Console.WriteLine("----");
                            Thread.Sleep(arguments.TimeInterval);                            
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






