﻿using NetMQ.Devices;
using System;

namespace Demo._06.QueueDevice {

    /// <summary>
    /// Refs
    /// https://github.com/NetMQ/NetMQ.Devices/blob/master/src/NetMQ.Devices/QueueDevice.cs
    /// https://github.com/NetMQ/NetMQ.Devices/blob/master/tests/NetMQ.Devices.Tests/QueueDeviceTests.cs
    /// </summary>
    class Program {

        const string defaultProxyEndPointFrontend = @"tcp://localhost:5678";
        const string defaultProxyEndPointBackend = @"tcp://*:5680";

        static void Main(string[] args) {
            
            string proxyEndPointFrontend = defaultProxyEndPointFrontend;
            string proxyEndPointBackend = defaultProxyEndPointBackend;

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            // tcp://localhost:5678 tcp://*:5680
            if (args.Length > 1) {
                proxyEndPointFrontend = args[0];
                proxyEndPointBackend = args[1];
            }

            var sharedQueue = new NetMQ.Devices.QueueDevice(
                proxyEndPointFrontend, 
                proxyEndPointBackend,
                DeviceMode.Blocking);

            Console.WriteLine($"starting {sharedQueue.GetType()} between {proxyEndPointFrontend} and {proxyEndPointBackend}");

            // this blocks because DeviceMode.Blocking
            sharedQueue.Start();

            // this message does not show on the console
            Console.WriteLine("shared queue started...");

            // this is not necessary 
            Console.ReadKey();
        }
    }
}
