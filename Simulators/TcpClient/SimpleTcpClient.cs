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
        private static Byte[] GenerateRandomSamples(MyArgs arg)
        {
            Random random = new Random();
            byte[] randomArray = new byte[arg.Sample];
            for (byte i = 0; i < randomArray.Length; i++)
            {
                randomArray[i] = (byte)random.Next(arg.MinValue, arg.MaxValue);
            }

            foreach (byte i in randomArray)
            {
                Console.WriteLine(i.ToString());
                //Thread.Sleep(arg.Interval);
            }
             
            return randomArray;
        }

        private static float[] GenerateLinearSignal (MyArgs arg)
        {            
            int x = arg.MaxValue - arg.MinValue;
            float rate = x / arg.TimeSample;
            float[] sampleLinearSignals = new float[arg.Sample];
            foreach (float i in sampleLinearSignals)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(r);
            }

            return sampleLinearSignals;
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

                        string message = "Message sent: Opened tcp:// localhost: " + endpoint.ToString();
                        clientSocket.Send(message, Encoding.UTF8);
                        Console.WriteLine(message);


                        string message2 = "Number of samples generated are " + arguments.Sample + " ranging between a min value of " + arguments.MinValue + " and a max value of " + arguments.MaxValue + " with a time interval of " + arguments.TimeSample + " milliseconds.";
                        clientSocket.Send(message2, Encoding.UTF8);
                        Console.WriteLine(message2);
                        
                        for (int t = 0; t < arguments.Repeat; t ++ )
                        {
                            //byte[] samplesGenerate = GenerateRandomSamples(arguments);

                            //foreach (int i in samplesGenerate)
                            //{
                            //    clientSocket.Send(" Sent Data " + i, Encoding.UTF8);
                            //}      

                            float[] sampleLinearSignal = GenerateLinearSignal(arguments);
                            foreach (float i in sampleLinearSignal)
                            {
                                clientSocket.Send(" Sent Data " + i, Encoding.UTF8);
                            }

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






