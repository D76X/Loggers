using System;
using ZeroMQ;
using ZeroMQ.Devices;

namespace Demo._04.ZeroMQ.Forwarder.XSUB.XPUB {

    /// <summary>
    /// Refs    
    /// https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ.AcceptanceTests/DeviceTests/Forwarder.cs
    /// https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Devices/ForwarderDevice.cs
    /// https://github.com/zeromq/clrzmq/blob/master/src/ZeroMQ/Devices/Device.cs
    /// </summary>
    class Program {

        static void Main(string[] args) {

            // both ends of the device must bind hence @"tcp://*:PORT"
            const string defaultProxyEndPointFrontend = @"tcp://*:5678";
            const string defaultProxyEndPointBackend = @"tcp://*:5680";

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine($"{args[i]}");
            }

            string proxyEndPointFrontend = defaultProxyEndPointFrontend;
            string proxyEndPointBackend = defaultProxyEndPointBackend;

            using (ZmqContext context = ZmqContext.Create()) {

                // you may also run the forwarder on a dedicated thread.
                ForwarderDevice forwarder = new ForwarderDevice(
                    context,
                    frontendBindAddr: proxyEndPointFrontend,
                    backendBindAddr: proxyEndPointBackend,
                    mode: DeviceMode.Blocking);

                // set up the forwarder as a pass through device
                forwarder.FrontendSetup.SubscribeAll();

                Console.WriteLine($"starting forwader from {proxyEndPointFrontend} to {proxyEndPointBackend}");

                // this blocks because mode: DeviceMode.Blocking 
                forwarder.Start();

                // you would see this only when mode: DeviceMode.Threaded
                Console.WriteLine("started");
                Console.ReadKey();

                Console.WriteLine("exiting...");
            }
        }
    }
}
