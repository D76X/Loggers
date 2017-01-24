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
            int newMinValue = Convert.ToInt32(arg.MinValue);
            int newMaxValue = Convert.ToInt32(arg.MaxValue);

            
            List<int> randomList = new List<int>();
            for (int i = newMinValue; i <= newMaxValue; i++)
            {
                randomList.Add(random.Next(newMinValue, newMaxValue));
            }

            foreach (int i in randomList)
            {
                Console.WriteLine(i.ToString());
            }
            return randomList;
        }

        private static List<int> DoubleToIntConverter( List<double> decValues)
        {
            List<int> resultList = new List<int>();
            int result;
            foreach (double value in decValues)
            { 
                try
                {
                    result = Convert.ToInt32(value);
                    resultList.Add(result);
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
            return resultList;
        }

        private static List<int> GenerateLinearSignal (MyArgs arg)
        {            
            double x = arg.MaxValue - arg.MinValue;
            double rate = x / arg.SampleNumber;
            List<double> samplesLinearSignal = new List<double>();
            for ( double i = arg.MinValue;i <=x; i = i+ rate  )
            {
                Console.WriteLine(i);
                samplesLinearSignal.Add(i);
                Thread.Sleep(arg.TimeSample);
            }
            
           return  DoubleToIntConverter(samplesLinearSignal);                       
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
                string endpoint = arguments.PortNumber.ToString();
                using (var context = ZmqContext.Create())
                {
                    using (var clientSocket = context.CreateSocket(SocketType.PUSH))
                    {
                        clientSocket.Connect("tcp://localhost:" + endpoint);

                        if (arguments.HelpPort !=null | arguments.HelpSample != null
                            | arguments.HelpRepeat != null | arguments.HelpTimeInterval != null| arguments.SignalType!=null)
                        {
                            Console.WriteLine(arguments.HelpPort);
                        
                            Console.WriteLine(arguments.HelpSample);
                        
                            Console.WriteLine(arguments.HelpRepeat);
                      
                            Console.WriteLine(arguments.HelpTimeInterval);                           
                        }
                                                
                        string message = "Message sent: Opened tcp:// localhost: " + endpoint.ToString();
                        clientSocket.Send(message, Encoding.UTF8);
                        Console.WriteLine(message);

                        string message2 = "You have chosen to generate a '" + arguments.SignalType + "' signal. Number of samples generated are " + arguments.SampleNumber + " ranging between a min value of " + arguments.MinValue + " and a max value of " + arguments.MaxValue + " with a time interval of " + arguments.TimeSample + " milliseconds.";
                        clientSocket.Send(message2, Encoding.UTF8);
                        Console.WriteLine(message2);
                        
                        for (int t = 0; t < arguments.Repeat; t ++ )
                        {
                            if (arguments.SignalType == "random")
                            {
                                List<int> intValues = GenerateRandomSamples(arguments);

                                foreach (int i in intValues)
                                {
                                    clientSocket.Send(" Sent Data " + i, Encoding.UTF8);
                                }
                            }

                            else if (arguments.SignalType == "linear")
                            {
                                List<int> intValues = GenerateLinearSignal(arguments);

                                foreach (int i in intValues)
                                {
                                    clientSocket.Send(" Sent Data " + i, Encoding.UTF8);
                                }
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






