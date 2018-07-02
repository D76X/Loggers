using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace Test.BandAgent1 {
    class Program {
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
        /// 
        /// You must set the language to C# 7.3 to get teh compiler to work with async Main.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args) {
            Console.WriteLine("Initializing Band Agent...");

            var device = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            await device.OpenAsync();

            Console.WriteLine("The device is connected!");

            // you can now send some messages from the device to teh iot-hub           
            int counter = 0;
            while (counter < 3) {

                // the message payload must be an array of bytes nbot a string!
                var message = new Message(Encoding.ASCII.GetBytes("Hello from Band Agent!"));

                await device.SendEventAsync(message);
                ++counter;

                Console.WriteLine("Sent message to IoT Hub!");

                // avoid overwhelming the hub
                Thread.Sleep(2000);
            }

            // you can send complex object too
            counter = 0;
            while (counter < 3) {

                var telemetry = new Telemetry() {
                    Message = "some telemetry data...",
                    StatusCode = 0
                };

                // you must serialize the object in some way i.e. xml or json
                // json is alway smaller in size
                var telemetryjson = JsonConvert.SerializeObject(telemetry);

                var message = new Message(Encoding.ASCII.GetBytes(telemetryjson));

                await device.SendEventAsync(message);
                ++counter;

                Console.WriteLine("Sent serialized objet to IoT Hub!");

                // avoid overwhelming the hub
                Thread.Sleep(2000);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
