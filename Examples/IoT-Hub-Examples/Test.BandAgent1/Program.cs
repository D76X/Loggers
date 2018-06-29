using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace Test.BandAgent1
{
    class Program
    {
        // this is the connection string of the device NOT the connection string of the hub!
        // the connection string used in the iothub-explorer is the connection string to let 
        // the iothub-explorer talk to the bus in order to query its properties. 
        // iothub-explorer lets you create a simulated device to also receive messages from 
        // and send messages to the hub. That simulated device does not neet a device connection
        // string but any other device needs one otherwise.
        // this connection string is provided by the IoT Hub to a registered device and it is         
        // how the IoT hub gets to know which device is trying to establish a connection with 
        // the Hub. 
        private const string DeviceConnectionString = @"HostName=psdemohub1.azure-devices.net;DeviceId=device-01;SharedAccessKey=RF82V2O/5usaLo01FQFTMYHZoDtLYRTU/FxUDv9Nm8A=";

        /// <summary>
        /// This program in .NET Core may be running on a device i.e. Android o Linux, etc.
        /// Its purpose is to establish a connection to the IoT Hub with which this device
        /// is registered.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing Band Agent...");

            var device = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            await device.OpenAsync();

            Console.WriteLine("The device is connected!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
