using Globomantics.Common;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.BandAgent4 {

    class Program {

        private const string DeviceConnectionString = @"HostName=psdemohub1.azure-devices.net;DeviceId=device-01;SharedAccessKey=RF82V2O/5usaLo01FQFTMYHZoDtLYRTU/FxUDv9Nm8A=";
        private static TwinCollection _reportedProperties = new TwinCollection();
        private static DeviceClient _device = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

        static async Task Main(string[] args) {

            Console.WriteLine("Initializing Band Agent...");

            await _device.OpenAsync();

            // the program running on the device may also listen for messages
            // sent by the IoT Hub. A background task may be used for it.
            var receiveEventsTask = ReceiveEvents(_device);

            //-----------------------------------------------------------------------------------
            // In this version of the AGENT code we implement the DIRECT METHOD AKA DEVICE METHOD 
            // pattern. The device can register an API exposed to any MANAGER application for 
            // direct communucation between the device and the manager. This is an alternative 
            // pattern to the AGENT-HUB-MANAGER pattern and does not need to be exclusive that is 
            // device and manager can use a mix of both patterns as it is the case in this 
            // implementation.

            // If the MANAGER invokes any unimplemented method on the device API (by mistake for ex)
            // it gets back a 502 by default. However, the device can provide a catch-all like this
            await _device.SetMethodDefaultHandlerAsync(HanldeUnimplementedDeviceDirecMethodCall, null);

            // the first parameter is the name of the method exposed as API to the manager
            // the second is the handler on teh device side that is run on the manager invokation
            object contextObject = null;
            await _device.SetMethodHandlerAsync("showMessage", HandleShowMessage, contextObject);

            //-----------------------------------------------------------------------------------

            Console.WriteLine("The device is connected!");

            // this D2C call notifies the IoT Hub that the device wishes to apply changes to 
            // the device twis on teh Hub.
            await UpdateTwin(_device);

            // here we set a callback for a device twins C2D notification.
            // The Hub may be triggered to push down to its devices this kind of updates i.e.
            // by a manager app when this happens and teh device in on-line the Hub sends down
            // the notification to teh device and the agent code running on it gets a chance to
            // process the notification in the callback.
            object context = null;
            await _device.SetDesiredPropertyUpdateCallbackAsync(UpdatePropertiesCallback, context);

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

                await _device.SendEventAsync(message);

                Console.WriteLine("Message sent!");
            }

            Console.WriteLine("Disconnecting...");
        }

        /// <summary>
        /// C2D device twins update.
        /// </summary>
        /// <param name="desiredProperties"></param>
        /// <param name="userContext"></param>
        /// <returns></returns>
        private static Task UpdatePropertiesCallback(
            TwinCollection desiredProperties, 
            object userContext) {

            // the context might contain some useful info...
            // in this case we know its null...

            // has the Hub notified that there is a firmaware update available?
            var currentFirmwareVersion = (string)_reportedProperties["firmwareVersion"];
            var desiredFirmwareVersion = (string)desiredProperties["firmwareVersion"];

            if (currentFirmwareVersion != desiredFirmwareVersion) {
                Console.WriteLine($"Firmware update requested.  Current version: '{currentFirmwareVersion}', " +
                                  $"requested version: '{desiredFirmwareVersion}'");

               ApplyFirmwareUpdate(desiredFirmwareVersion);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Simulates a firmware update on the device.
        /// </summary>
        /// <param name="targetVersion"></param>
        /// <returns></returns>
        private static async Task ApplyFirmwareUpdate(string targetVersion) {

            Console.WriteLine("Beginning firmware update...");

            _reportedProperties["firmwareUpdateStatus"] =
                $"Downloading zip file for firmware {targetVersion}...";
            // keep the Hub in the known about what the device is doing 
            // by updating the device twins...
            await _device.UpdateReportedPropertiesAsync(_reportedProperties);

            // simulate dowload
            Thread.Sleep(5000);

            _reportedProperties["firmwareUpdateStatus"] = "Unzipping package...";
            await _device.UpdateReportedPropertiesAsync(_reportedProperties);

            // simulate unzipping
            Thread.Sleep(5000);

            _reportedProperties["firmwareUpdateStatus"] = "Applying update...";
            await _device.UpdateReportedPropertiesAsync(_reportedProperties);

            // simulate applying
            Thread.Sleep(5000);

            Console.WriteLine("Firmware update complete!");

            _reportedProperties["firmwareUpdateStatus"] = "n/a";
            _reportedProperties["firmwareVersion"] = targetVersion;
            await _device.UpdateReportedPropertiesAsync(_reportedProperties);
        }

        /// <summary>
        /// D2C device twins update.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private static async Task UpdateTwin(DeviceClient device) {

            // !!!
            // Careful this will cause an error because there is a .
            // in the name of the property which is not an allowed 
            // character.
            // twinProperties["connection.type"] = "wi-fi";

            // for example here we are telling the hub that this device
            // is now in wi-fi mode and the strngth of its connection 
            // is good
            _reportedProperties["connectionType"] = "wi-fi";
            _reportedProperties["connectionStrength"] = "full";

            // send it up to the Hub
            await device.UpdateReportedPropertiesAsync(_reportedProperties);
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

        /// <summary>
        /// When the MANAGER invokes an unimplememted direct method call on
        /// the device this catch-all is registered on the device.
        /// </summary>
        /// <param name="methodRequest"></param>
        /// <param name="userContext"></param>
        /// <returns></returns>
        private static Task<MethodResponse> HanldeUnimplementedDeviceDirecMethodCall(
            MethodRequest methodRequest, 
            object userContext) {

            Console.WriteLine("****OTHER DEVICE METHOD CALLED****");
            Console.WriteLine($"Method: {methodRequest.Name}");
            Console.WriteLine($"Payload: {methodRequest.DataAsJson}");

            var responsePayload = Encoding.ASCII.GetBytes("{\"response\": \"The method is not implemented!\"}");

            int statusCode = 404;
            return Task.FromResult(new MethodResponse(responsePayload,statusCode));
        }

        /// <summary>
        /// This method handles the invokation of a DIRECT message 
        /// invokation on the manager.
        /// </summary>
        /// <param name="methodRequest"></param>
        /// <param name="userContext"></param>
        /// <returns></returns>
        private static Task<MethodResponse> HandleShowMessage(
            MethodRequest methodRequest, 
            object userContext) {

            // the context can be used to pass any sort of data 
            // from the manager to the device on a direct invokation

            // ... do something with the context...

            // on a real device we would unpack the payload and then
            // use it to make calls to the device api to perform
            // tasks such as updates, display messages, etc.

            Console.WriteLine("***MESSAGE RECEIVED***");
            Console.WriteLine(methodRequest.DataAsJson);

            // a MANAGER to DEVICE call MUST provide immediate 
            // feedback to the device. Any byte array of a 
            // serialized POCO object may be used here.
            var responsePayload = Encoding.ASCII.GetBytes("{\"response\": \"Message shown!\"}");

            //...do something with the payload and then
            // figure out a status code to return to the
            // manager together with appropriate payload...

            // It is possible to use any kind of status code
            // HTTP status codes are a sensible option.
            int statusCode = 200;
            return Task.FromResult(new MethodResponse(responsePayload, statusCode));
        }
    }
}

