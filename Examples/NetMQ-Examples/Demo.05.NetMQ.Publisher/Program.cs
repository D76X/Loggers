using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Demo._05.NetMQ.Publisher {

    class Program {

        internal class AreaData {

            internal readonly DateTime SampleTime;
            internal readonly int Temperature;
            internal readonly int Pressure;
            internal readonly int Humidity;
            internal readonly bool Ventilation;
            internal readonly bool Conditioning;

            public AreaData(
                DateTime sampleTime,
                int temperature,
                int pressure,
                int humidity,
                bool ventilation,
                bool conditioning) {

                this.SampleTime = sampleTime;
                this.Temperature = temperature;
                this.Pressure = pressure;
                this.Humidity = humidity;
                this.Ventilation = ventilation;
                this.Conditioning = conditioning;
            }
        }

        internal class AreaDataReader {

            private Random rnd = new Random();
            internal AreaData GetSample() =>
                new AreaData(
                    DateTime.Now,
                    rnd.Next(301),
                    rnd.Next(11),
                    rnd.Next(101),
                    rnd.NextDouble() >= 0.5,
                    rnd.NextDouble() >= 0.5);
        }

        internal static class AreaMessageFactory {

            private static NetMQMessage CreateMessage(
                string topic,
                string originEndPoint,
                AreaData data) {

                NetMQMessage msg = new NetMQMessage();
                msg.Append(topic);
                msg.Append(originEndPoint);
                msg.Append(new NetMQFrame(BitConverter.GetBytes(data.SampleTime.Ticks)));

                switch (topic) {
                    case topicAll:
                        msg.Append(data.Temperature);
                        msg.Append(data.Pressure);
                        msg.Append(data.Humidity);
                        msg.Append(new NetMQFrame(BitConverter.GetBytes(data.Ventilation)));
                        msg.Append(new NetMQFrame(BitConverter.GetBytes(data.Conditioning)));
                        break;
                    case topicTemperature:
                        msg.Append(data.Temperature);
                        break;
                    case topicPressure:
                        msg.Append(data.Pressure);
                        break;
                    case topicHumidity:
                        msg.Append(data.Humidity);
                        break;
                    case topicVentialation:
                        msg.Append(new NetMQFrame(BitConverter.GetBytes(data.Ventilation)));
                        break;
                    case topicConditioning:
                        msg.Append(new NetMQFrame(BitConverter.GetBytes(data.Conditioning)));
                        break;
                    default:
                        break;
                }

                return msg;
            }

            internal static List<NetMQMessage> CreateMessages(
                string originEndPoint,
                AreaDataReader areaDataReader,
                IEnumerable<string> topics) {

                AreaData sample = areaDataReader.GetSample();
                var msgs = new List<NetMQMessage>();

                if (topics.Contains(topicAll)) {

                    msgs.Add(CreateMessage(topicAll, originEndPoint, sample));

                }
                else {

                    foreach (var topic in topics) {
                        msgs.Add(CreateMessage(topic, originEndPoint, sample));
                    }
                }

                return msgs;
            }
        }

        internal static class MessageLogger {

            internal static void Log(NetMQMessage message) {

                var enumerator = message.GetEnumerator();
                enumerator.MoveNext();

                string topic = Encoding.UTF8.GetString(enumerator.Current.Buffer);
                enumerator.MoveNext();

                string ep = Encoding.UTF8.GetString(enumerator.Current.Buffer);
                enumerator.MoveNext();

                DateTime st = DateTime.FromBinary(BitConverter.ToInt64(enumerator.Current.Buffer, 0)); ;
                enumerator.MoveNext();

                int t = BitConverter.ToInt32(enumerator.Current.Buffer.Reverse().ToArray(), 0);
                enumerator.MoveNext();

                int p = BitConverter.ToInt32(enumerator.Current.Buffer.Reverse().ToArray(), 0);
                enumerator.MoveNext();

                int h = BitConverter.ToInt32(enumerator.Current.Buffer.Reverse().ToArray(), 0);
                enumerator.MoveNext();

                bool v = BitConverter.ToBoolean(enumerator.Current.Buffer, 0);
                enumerator.MoveNext();

                bool c = BitConverter.ToBoolean(enumerator.Current.Buffer, 0);

                Console.WriteLine($"topic = {topic}, ep = {ep}, st = {st}, t = {t}, p = {p}, h = {h}, v = {v}, c = {c}");
            }
        }

        const string defaultProxyEndPoint = @"tcp://localhost:5678";
        const int defaultSamplingTime = 1000;
        const string topicAll = "ALL";
        const string topicTemperature = @"TEMPERAURE";
        const string topicPressure = @"PRESSURE";
        const string topicHumidity = @"HUMIDITY";
        const string topicVentialation = @"VENTIALTION";
        const string topicConditioning = @"CONDITIONING";

        static void Main(string[] args) {

            string proxyEndPoint = defaultProxyEndPoint;
            int samplingTime = defaultSamplingTime;
            List<string> topics = new List<string>();

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678 1000 ALL
            // tcp://localhost:5678 500 TEMPERATURE PRESSURE
            if (args.Length > 1) {

                proxyEndPoint = args[0];
                samplingTime = int.Parse(args[1]);

                for (int i = 2; i < args.Length; i++) {
                    topics.Add(args[i]);
                }
            }
            else {
                topics.Add(topicAll);
            }

            using (PublisherSocket publisher = new PublisherSocket()) {

                publisher.Options.SendHighWatermark = 1000;
                publisher.Connect(proxyEndPoint);
                Console.WriteLine($"Publisher socket connecting to proxy on {proxyEndPoint}");                
                Console.WriteLine();

                var areaDataReader = new AreaDataReader();
                bool pause = false;

                while (true) {

                    if (!pause) {
                        SendData(publisher, proxyEndPoint, areaDataReader, topics);
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

                    Thread.Sleep(samplingTime);
                }
            }
        }

        private static void SendData(
            PublisherSocket publisher,
            string originEndPoint,
            AreaDataReader areaDataReader,
            IEnumerable<string> topics) {

            List<NetMQMessage> messages = AreaMessageFactory
                    .CreateMessages(
                        originEndPoint,
                        areaDataReader,
                        topics);

            foreach (var message in messages) {
                publisher.SendMultipartMessage(message);
                MessageLogger.Log(message);
            }
        }
    }
}

