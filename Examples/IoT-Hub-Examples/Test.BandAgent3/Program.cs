﻿using Globomantics.Common;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Test.BandAgent3 {

    class Program {

        private const string DeviceConnectionString = @"HostName=psdemohub1.azure-devices.net;DeviceId=device-01;SharedAccessKey=RF82V2O/5usaLo01FQFTMYHZoDtLYRTU/FxUDv9Nm8A=";

        static async Task Main(string[] args) {

            Console.WriteLine("Initializing Band Agent...");

            var device = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            await device.OpenAsync();

            // the program running on the device may also listen for messages
            // sent by the IoT Hub. A background task may be used for it.
            var receiveEventsTask = ReceiveEvents(device);

            Console.WriteLine("The device is connected!");

            await UpdateTwin(device);

            // these are the options available on teh band device
            // here a simple menu is printed to the console to 
            // simulate the set of buttons/controls that would be 
            // available on teh real device.
            Console.WriteLine("Press a key to perform an action:");
            Console.WriteLine("q: quits");
            Console.WriteLine("h: send happy feedback");
            Console.WriteLine("u: send unhappy feedback");
            Console.WriteLine("e: request emergency help");

            var random = new Random();
            var quitRequested = false;

            while (!quitRequested) {

                Console.Write("Action? ");
                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                // some telemetry data to be setn up to the
                // IoT Hub
                var status = StatusType.NotSpecified;
                // in a read device the .NET Core app would
                // read latitude and longitude from the GPS
                // sensor/chip.
                var latitude = random.Next(0, 100);
                var longitude = random.Next(0, 100);

                // this corresponds to any options selected
                // by the user on teh device buttons
                switch (Char.ToLower(input)) {
                    case 'q':
                        quitRequested = true;
                        break;
                    case 'h':
                        status = StatusType.Happy;
                        break;
                    case 'u':
                        status = StatusType.Unhappy;
                        break;
                    case 'e':
                        status = StatusType.Emergency;
                        break;
                }

                // this is a POCO that can be serilized and 
                // sent up to the IoT Hub as payload of message.
                // The Hub will queue this up in ne of the available
                // partition where the message then waits to be picked 
                // up by a processor.
                var telemetry = new Telemetry {
                    Latitude = latitude,
                    Longitude = longitude,
                    Status = status
                };

                // this is a good way to serialize the POCO, better
                // than the default XML as it result in a shorter
                // byte array. It can also be compressed further
                // but for now we skip this.
                var payload = JsonConvert.SerializeObject(telemetry);

                // the payload must be wrapped-up in an IoT Hub message
                // before sending it. Notice that the serialized object 
                // must be converted into its byte representation before
                // giving it to the Message class constructor.
                var message = new Message(Encoding.ASCII.GetBytes(payload));

                await device.SendEventAsync(message);

                Console.WriteLine("Message sent!");
            }

            Console.WriteLine("Disconnecting...");
        }

        private static async Task UpdateTwin(DeviceClient device) {

            var twinProperties = new TwinCollection();

            // Careful this will cause an error because there is a .
            // in the name of the property which is not an allowed 
            // character.
            // twinProperties["connection.type"] = "wi-fi";

            twinProperties["connectionType"] = "wi-fi";
            twinProperties["connectionStrength"] = "full";

            await device.UpdateReportedPropertiesAsync(twinProperties);
            Console.WriteLine("sent update to device twin properties");
        }

        private static async Task ReceiveEvents(DeviceClient device) {

            while (true) {

                bool? errorWhileProcessingTheMessage = null;

                // ReceiveAsync returns as soon as there is a message
                // to read from the hub or after a timeout expires.
                var message = await device.ReceiveAsync();

                if (message == null) {
                    // the timeout has expired there are no 
                    // messages from the hub
                    continue;
                }

                var messageBody = message.GetBytes();

                // as usual the payload is a byte array that may be 
                // a serialized object
                var payload = Encoding.ASCII.GetString(messageBody);

                // on a real device the payload would be thrown into the 
                // device API to do things i.e. display messages or activate
                // functions if the meaning of the message from the Hub is a 
                // command.
                Console.WriteLine($"Received message from cloud: '{payload}'");
                errorWhileProcessingTheMessage = false;

                if (!errorWhileProcessingTheMessage.HasValue) {

                    // the device might not be able to attend to the ops
                    // implied by the message sent by the Hub right now 
                    // so it abandons it to try later...
                    await device.AbandonAsync(message);
                }

                if (!errorWhileProcessingTheMessage.Value) {

                    // this is teh device accepting the message as all was
                    // good with it
                    await device.CompleteAsync(message);
                }
                else {
                    // the device can also reject the message...
                    await device.RejectAsync(message);
                }
                
            }
        }
    }
}

