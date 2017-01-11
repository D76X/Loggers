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
        private static List<int> GenerateRandomSamples(MyArgs arg)
        {
            Random random = new Random();
            List<int> randomList = new List<int>();
            for (int i = 0; i < randomList.Count(); i++)
            {
                randomList.Add(random.Next(arg.MinValue, arg.MaxValue));
            }

            foreach (int i in randomList)
            {
                Console.WriteLine(i.ToString());
                //Thread.Sleep(arg.Interval);
            }
             
            return randomList;
        }

        private static void DecimalToIntConverter( List<decimal> decValues)
        {
            int result;
            foreach (decimal value in decValues)
            { 
                try
                {
                    result = Convert.ToInt32(value);
                    Console.WriteLine("Converted the {0} value '{1}' to the {2} value {3}.",
                        value.GetType().Name, value, 
                        result.GetType().Name, result);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("{0} is outside the range of the Int32 type.",
                        value);
                }
            }
        }

        private static void GenerateLinearSignal (MyArgs arg)
        {            
            int x = arg.MaxValue - arg.MinValue;
            decimal rate = x / arg.Sample;
            List<decimal> samplesLinearSignal = new List<decimal>();
            for ( decimal i = arg.MinValue;i <=x; i = i+ rate  )
            {
                Console.WriteLine(i);
                samplesLinearSignal.Add(i);
            }
            foreach (decimal i in samplesLinearSignal)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(arg.TimeSample);
            }

            DecimalToIntConverter(samplesLinearSignal);
           // return samplesLinearSignal;             
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

                             GenerateLinearSignal(arguments);

                            //foreach (int i in sampleLinearSignal)
                            //{
                            //    clientSocket.Send(" Sent Data " + i, Encoding.UTF8);
                            //}

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






