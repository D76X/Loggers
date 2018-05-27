using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo._05.NetMQ.Subscriber {

    class Program {

        const string defaultProxyEndPoint = @"tcp://localhost:5680";
        const int defaultExpectedMessageFrames = 8;
        const string topicAll = "ALL";
        const string topicTemperature = @"TEMPERATURE";
        const string topicPressure = @"PRESSURE";
        const string topicHumidity = @"HUMIDITY";
        const string topicVentialation = @"VENTILATION";
        const string topicConditioning = @"CONDITIONING";

        internal static class MessageLogger {

            internal static void Log(NetMQMessage message) {

                string topic = message[0].ConvertToString();
                string origin = message[1].ConvertToString();
                DateTime st = DateTime.FromBinary(BitConverter.ToInt64(message[2].Buffer, 0));

                string head = $"topic = {topic}, origin = {origin}, st = {st}";

                switch (topic) {

                    case topicAll: {
                            int t = message[3].ConvertToInt32();
                            int p = message[4].ConvertToInt32();
                            int h = message[5].ConvertToInt32();
                            bool v = BitConverter.ToBoolean(message[6].Buffer, 0);
                            bool c = BitConverter.ToBoolean(message[7].Buffer, 0);
                            Console.WriteLine($"{head}, t = {t}, p = {p}, h = {h}, v = {v}, c = {c}");
                        }
                        break;
                    case topicTemperature: {
                            int t = message[3].ConvertToInt32();
                            Console.WriteLine($"{head}, t = {t}");
                        }
                        break;
                    case topicPressure: {
                            int p = message[3].ConvertToInt32();
                            Console.WriteLine($"{head}, p = {p}");
                        }
                        break;
                    case topicHumidity: {
                            int h = message[3].ConvertToInt32();
                            Console.WriteLine($"{head}, h = {h}");
                        }
                        break;
                    case topicVentialation: {
                            bool v = BitConverter.ToBoolean(message[3].Buffer, 0);
                            Console.WriteLine($"{head}, v = {v}");
                        }
                        break;
                    case topicConditioning: {
                            bool c = BitConverter.ToBoolean(message[3].Buffer, 0);
                            Console.WriteLine($"{head}, v = {c}");
                        }
                        break;
                    default:
                        Console.WriteLine($"unexpected message format with topic {topic}");
                        break;
                }
            }

            static void Main(string[] args) {

                string proxyEndPoint = defaultProxyEndPoint;
                List<string> topics = new List<string>();
                int expectedMessageFrames = defaultExpectedMessageFrames;

                for (int i = 0; i < args.Length; i++) {
                    Console.WriteLine($"{args[i]}");
                }

                // tcp://localhost:5680 2 TEMPERATURE PRESSURE
                if (args.Length > 1) {

                    proxyEndPoint = args[0];
                    expectedMessageFrames = int.Parse(args[1]);

                    for (int i = 2; i < args.Length; i++) {
                        topics.Add(args[i]);
                    }
                }
                else {
                    topics.Add(topicAll);
                }

                string subscription = TopicsToSubcription(topics);
                bool unsubscribed = false;

                using (var subscriber = new SubscriberSocket()) {

                    subscriber.Options.ReceiveHighWatermark = 5;

                    subscriber.Connect(proxyEndPoint);
                    Console.WriteLine($"subscriber socket connecting on {proxyEndPoint}");

                    subscriber.Subscribe(subscription);
                    Console.WriteLine($"subscribed to {subscription}");

                    while (true) {

                        if (!unsubscribed) {
                            ReadMessage(subscriber, expectedMessageFrames);                            
                        }

                        if (Console.KeyAvailable) {

                            if (!unsubscribed && Console.ReadKey(true).Key == ConsoleKey.P) {
                                subscriber.Unsubscribe(subscription);
                                unsubscribed = true;
                                Console.WriteLine($"unsubribed from {subscription}");
                                Console.WriteLine("press any key but P to resubscribe...");
                            }
                            else {
                                subscriber.Subscribe(subscription);
                                unsubscribed = false;
                                Console.WriteLine($"resubscribed to {subscription}");
                            }
                        }                        
                    }
                }
            }

            static string TopicsToSubcription(IEnumerable<string> topics) {

                if (topics.Contains(topicAll)) {
                    return $"{topicAll}";
                }

                var sb = new StringBuilder();

                foreach (var topic in topics) {
                    sb.Append($"{topic} ");
                }

                return sb.ToString().TrimEnd();
            }

            static void ReadMessage(
                SubscriberSocket subscriber,
                int expectedMessageFrames) {

                NetMQMessage message = subscriber
                    .ReceiveMultipartMessage(expectedMessageFrames);

                MessageLogger.Log(message);
            }
        }
    }
}
