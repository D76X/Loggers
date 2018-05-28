using System;
using System.Text;
using System.Threading;
using ZeroMQ;

namespace Demo._04.ZeroMQ.Publisher2.PUB {

    /// <summary>
    /// Illustrate the case of a publisher endpoint which publishes 
    /// multiple message types.
    /// </summary>
    class Program {

        internal class Area2DataReader {

            private Random rnd = new Random();

            internal int ReadTemperature() {
                Thread.Sleep(100);
                return this.rnd.Next(301);
            }

            internal int ReadPressure() {
                Thread.Sleep(100);
                return this.rnd.Next(11);
            }

            internal int ReadHumidity() {
                Thread.Sleep(100);
                return this.rnd.Next(101);
            }

            internal bool ReadVentilation() {
                Thread.Sleep(100);
                return this.rnd.NextDouble() >= 0.5;
            }

            internal bool ReadAirConditioning() {
                Thread.Sleep(100);
                return this.rnd.NextDouble() >= 0.5;
            }
        }

        private const string defaultEndPointArea2 = @"tcp://*:11001";
        private const string keyTemperature = @"AREA2_TEMPEREATURE";
        private const string keyPressure = @"AREA2_PRESSURE";
        private const string keyHumidity = @"AREA2_HUMIDITY";
        private const string keyVentilation = @"AREA2_VENTILATION";
        private const string keyAirConditioning = @"AREA2_AIR_CONDITIONING";

        static void Main(string[] args) {

            string endPointArea2 = defaultEndPointArea2;
            bool connectToProxy = false;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            if (args.Length == 1) {
                endPointArea2 = args[0];               
            }

            if (!endPointArea2.Contains("*")) {
                connectToProxy = true;
            }

            Console.WriteLine("press P to pause publishing");
            Console.WriteLine();

            using (ZmqContext context = ZmqContext.Create())
            using (ZmqSocket publisherArea2 = context.CreateSocket(SocketType.PUB)) {

                if (connectToProxy) {

                    publisherArea2.Connect(endPointArea2);
                    Console.WriteLine("Publisher for Area 2 connected to proxy {0}", endPointArea2);

                } else {

                    publisherArea2.Bind(endPointArea2);
                    Console.WriteLine("Publisher for Area 2 bound on to {0}", endPointArea2);
                }

                var dataReaderArea2 = new Area2DataReader();
                bool pause = false;

                while (true) {

                    if (!pause) {

                        SendDataArea2(publisherArea2, dataReaderArea2);
                    }

                    if (Console.KeyAvailable) {

                        if (!pause && Console.ReadKey(true).Key == ConsoleKey.P) {
                            pause = true;
                            Console.WriteLine("publishing has been paused press any key but P to continue...");
                        }
                        else {
                            pause = false;
                        }
                    }

                    Thread.Sleep(1000);
                }

            }
        }

        private static void SendDataArea2(
            ZmqSocket publisherSocket,
            Area2DataReader areaDataReader) {

            var t = $"{keyTemperature} {DateTime.Now} {areaDataReader.ReadTemperature()}";
            publisherSocket.Send(t, Encoding.UTF8);
            Console.WriteLine(t);

            var p = $"{keyPressure} {DateTime.Now} {areaDataReader.ReadPressure()}";
            publisherSocket.Send(p, Encoding.UTF8);
            Console.WriteLine(p);

            var h = $"{keyHumidity} {DateTime.Now} {areaDataReader.ReadHumidity()}";
            publisherSocket.Send(h, Encoding.UTF8);
            Console.WriteLine(h);

            var v = $"{keyVentilation} {DateTime.Now} {areaDataReader.ReadVentilation()}";
            publisherSocket.Send(v, Encoding.UTF8);
            Console.WriteLine(v);

            var c = $"{keyAirConditioning} {DateTime.Now} {areaDataReader.ReadAirConditioning()}";
            publisherSocket.Send(c, Encoding.UTF8);
            Console.WriteLine(c);
        }
    }
}
