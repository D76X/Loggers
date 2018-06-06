using NetMQ.Devices;
using System;

namespace Demo._07.StreamerDevice {

    /// <summary>
    /// Refs
    /// https://github.com/NetMQ/NetMQ.Devices/blob/master/src/NetMQ.Devices/StreamerDevice.cs
    /// </summary>
    class Program {

        // A streamer device is a PULL-PUSH socktes proxy.
        // It collects tasks from a set of pushers and forwards these to a set of pullers.
        // It is a solution to the problem of Dynamic Discovery. 
        // It binds on the frontend from which it PULLs messages.
        // It binds on the backend to which is PUSHes messages.       

        const string defaultFrontendEndPoint = @"tcp://*:5678";
        const string defaultBackendEndPoint = @"tcp://*:5680";                

        static void Main(string[] args) {

            string frontendEndPoint = defaultFrontendEndPoint;
            string backendEndPoint = defaultBackendEndPoint;

            // tcp://*:5678 tcp://*:5680
            if (args.Length > 1) {
                frontendEndPoint = args[0];
                backendEndPoint = args[1];
            }

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            var streamerDevice = new NetMQ.Devices
                .StreamerDevice(
                frontendEndPoint, 
                backendEndPoint, 
                DeviceMode.Blocking);

            // this blocks because DeviceMode.Blocking
            streamerDevice.Start();

            // this message does not show on the console
            Console.WriteLine("streamer device started...");

            // this is not necessary 
            Console.ReadKey();
        }
    }
}
