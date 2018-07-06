using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.BandManager {

    /// <summary>
    /// 
    /// A manager is .NET Core application that can tell the IoT Hub to 
    /// send messages to its devices.
    /// 
    /// The manager sends messages to the Hub addressed to devices that 
    /// are registered on the Hub so that teh devices can pick them up.
    /// 
    /// Messages sent from Cloud to Device may be notifications, commands,
    /// updates, etc.
    /// 
    /// For this .NET Core to work we need the Microsoft.Azure.Device pks
    /// which is different from teh Microsoft.Azure.Device.Client that is 
    /// used in the agent apps that run on the device.
    /// </summary>
    class Program {

        // You get this from the Azure portal in Shared Access Polices for the 
        // Hub. This will be the Service Connection String. This string allows
        // the Hub to send messages to the device.
        private const string ServiceConnectionString = @"HostName=psdemohub1.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=7nTmG5YbixIy8iNhcrSRyCJURUIS6zsgIViSyj6JjDM=";

        static async Task Main(string[] args) {

            // to send messages from teh Hub to the device we use a ServiceClient.
            var serviceClient = ServiceClient.CreateFromConnectionString(ServiceConnectionString);

            // to be able to send C2D message to notify devices of updates to the
            // device twins properties we must use a dedicated class.
            // !WARNING!
            // Here we use the same connection string of the service which by default
            // does not grant permission to read and write to the device twins. 
            // You can change this from teh Azure portale in Access Policy if you 
            // wish. although a dedicated connection string for the registry manger 
            // would be a better choice. (How do I do that?)
            var registryManager = RegistryManager.CreateFromConnectionString(ServiceConnectionString);

            // In the same way as in the agent apps that run on the device where
            // a thread can be used to listen to messages sent by the IoT Hub to 
            // the device (C2D) here in the manager app we can do use a thread to 
            // listen to the feedback messages that the devices may send back to 
            // the IoT Hub in reply to the C2D messages i.e. AKW, rejection or
            // abandoned, etc. 
            // Without feedback to the manager the C2D channel is a one-way comm
            // and in some cases this might be a suitable comm model i.e. when 
            // simple notification are sent to the device in order to be displayed 
            // to the users on their GUIs but there are scenarios in which the 
            // manager must know what the device has done or will do with the 
            // message it has received or whether it has received the message at 
            // all, etc. For example the manager might want to notify the device
            // that a firmware update is available thus the manager must get a AKW 
            // from the device to make sure that it has not been left out.
            var feedbackTask = ReceiveFeedback(serviceClient);

            var quitRequested = false;
            while (!quitRequested) {

                Console.WriteLine("Which device do you wish to send a message to? ");
                Console.Write("> ");
                var deviceId = Console.ReadLine();

                Console.WriteLine("Which pattern would you like to use to send the message?");

                // these are the options available to a MANAGER app
                // to communicate with the device (C2D)
                Console.WriteLine("Press a key to perform an action:");
                Console.WriteLine("q: quits");
                Console.WriteLine("s: send message agent-hub-device pattern");
                Console.WriteLine("d: call direct method pattern");
                Console.WriteLine("t: send device twins update message");
                var input = Console.ReadKey().KeyChar;

                // this corresponds to any options selected
                // by the user on teh device buttons
                switch (Char.ToLower(input)) {
                    case 'q':
                        quitRequested = true;
                        break;
                    case 's':
                        //--------------------------------------------------------------------
                        // obviously this sends a message straight to the device
                        // but it does so according to the AGENT-HUB-MANAGER pattern
                        await SendCloudToDeviceMessage(serviceClient, deviceId);
                        //--------------------------------------------------------------------
                        break;
                    case 'd':
                        //--------------------------------------------------------------------
                        // The following is the implementation of the alternative pattern 
                        // DIRECT METHOD AKA DEVICE METHOD
                        await CallDirectMethod(serviceClient, deviceId);
                        //--------------------------------------------------------------------
                        break;
                    case 't':
                        //--------------------------------------------------------------------
                        // The foolowing is the implementation of the device twins pattern
                        await UpdateDeviceFirmware(registryManager, deviceId);
                        //--------------------------------------------------------------------
                        break;
                    default:
                        Console.WriteLine($"{input} is not a valid option");
                        break;
                }
            }

            Console.WriteLine("exiting...");
        }

        /// <summary>
        /// Manager side part of the DEVICE TWINS communication pattern
        /// </summary>
        /// <param name="registryManager"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private static async Task UpdateDeviceFirmware(
            RegistryManager registryManager,
            string deviceId) { //,
                               //string firmVer) {

            // get the twins for the device
            // the registry manager has permission to do so...

            var deviceTwin = await registryManager.GetTwinAsync(deviceId);
            
            // look into the reported properties to find the firmwareVersion
            string initialVersion = deviceTwin.Properties.Reported["firmwareVersion"];

            // work out teh next firmware version i.e. read it from a DB or URL
            // here we just add 1.0 or set to 1.0 if the firmwareVersion has not 
            // been found.
            string updateVersion = initialVersion == "" ? 
                "1.0" :
                (Convert.ToDouble(initialVersion) + 1.0).ToString("N1");

            // create a patch model...
            // a POCO to model the firmware update
            // here we want the cloud side of things to send a message to 
            // a device to notify it of a change to the desired properties
            // of its twins. The device will get the message as soon as it
            // goes online and then decides what actions to take if any.
            //------------------------------------------------------------
            // In this version of the Microsoft.Azure.Device pck used by 
            // the agent there is no type safety on the POCO for the twins 
            // patch. This is bad and mightbe improved in future releases
            // of teh package. For the moment you must use nested anonymous 
            // objects to model the device twins patch.
            //------------------------------------------------------------
            var twinPatch = new {
                properties = new {
                    desired = new {
                        firmwareVersion = updateVersion 
                    }
                }
            };

            // the payload of the message in this pattern must be JSON.
            var twinPatchJson = JsonConvert.SerializeObject(twinPatch);

            // a registry manager must be used in this communication pattern
            // a registry manager normaly uses a dedicated connection string 
            // that is not the ServiceConnectionString used by the service client
            // has ServiceConnectionString will normally not have permission to
            // read and write to teh device twins, although it may be granted such
            // permission from the Azure portal.
            // Notice that the ETAG of the twins is sent into the message to deal 
            // with concurrency. The update will succeed only if the ETag on the 
            // message matches up with that on the document at the time of delivery. 
            // more work required...
            await registryManager.UpdateTwinAsync(
                deviceId, 
                twinPatchJson, 
                deviceTwin.ETag);

            Console.WriteLine($"Firmware update sent to device '{deviceId}'...");

            
            // this is a simplified monitor we should have ti in its onw task...

            // Now that the device has been notified of the firmware update the 
            // cloud side that is the manager wants to know what the device is 
            // doing is anything...           
            
            while (true) {

                // take 1 sec break
                Thread.Sleep(1000);

                // as the device goes through tapplying the updates it post 
                // changes to the "firmwareUpdateStatus" property of its twins
                // thus the manager can check that to monitor teh progress of
                // the updates on teh device.
                deviceTwin = await registryManager.GetTwinAsync(deviceId);

                Console.WriteLine($"Firmware update status: {deviceTwin.Properties.Reported["firmwareUpdateStatus"]}");

                if (deviceTwin.Properties.Reported["firmwareVersion"] == updateVersion) {
                    Console.WriteLine("Firmware update complete!");
                    break;

                }
            }
        }

        /// <summary>
        /// Manager side part of the DIRECT METHOD AKA DEVICE METHOD pattern 
        /// </summary>
        /// <param name="serviceClient"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private static async Task CallDirectMethod(
            ServiceClient serviceClient,
            string deviceId) {

            // the manager must know the name of the methods 
            // exposed by the device API for the DIRECT METHOD
            // invokation.
            // In this case the manager assumes the device has 
            // registered a method named "showMessage" as part
            // of its DIRECT METHOD API.
            var method = new CloudToDeviceMethod("showMessage");

            // any valid JSON can be used as payload
            // this is why there are '' within ""
            // of course in general you would send over JSON
            // serialized POCOs but this would do for now.
            method.SetPayloadJson("'Hello from C#'");

            // with the DIRECT METHOD the manager gets a feedback message 
            // immediately it is a request-reply pattern
            var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, method);

            Console.WriteLine($"Response status: {response.Status}, payload: {response.GetPayloadAsJson()}");

        }

        /// <summary>
        /// Manager side part of the AGENT-HUB-MANAGER pattern
        /// implementation C2D.
        /// </summary>
        /// <param name="serviceClient"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private static async Task SendCloudToDeviceMessage(
            ServiceClient serviceClient,
            string deviceId) {

            Console.WriteLine("What message payload do you want to send? ");
            Console.Write("> ");

            var payload = Console.ReadLine();

            // as usual the payload must be teh byte array of a serialized object.
            var commandMessage = new Message(Encoding.ASCII.GetBytes(payload));

            // these are important!

            // a C2D message can hold a ID which might be useful 
            // to the manager when it processes the feedback msg
            // coming back from the device through the Hub.
            commandMessage.MessageId = Guid.NewGuid().ToString();

            // this asks the devive to aknowledge the reception of the message
            // without this the manager would work as fire-and-forget pattern
            // with it instead it is a request-reply pattern. 
            // Consider the scenario in which a device is temporary off-line
            // and the manager must send an important message to it i.e. to 
            // schedule a firmware update. In this scenario you would want to
            // make sure the manager gets an AKW of the message...
            // Another scenario is that of the device being on-line but busy 
            // and unable to perform any operation, the same applies here...

            // Full = a feedback message D2C is generated if the device
            // accepts it or reject it.
            commandMessage.Ack = DeliveryAcknowledgement.Full;

            // The Hub will stop trying to deliver the message to the 
            // device if it does so unsuccessfully for 10 seconds
            commandMessage.ExpiryTimeUtc = DateTime.UtcNow.AddSeconds(10);

            await serviceClient.SendAsync(deviceId, commandMessage);
        }

        /// <summary>
        /// Manager side part of the AGENT-HUB-MANAGER pattern
        /// implementation D2C.
        /// </summary>
        /// <param name="serviceClient"></param>
        /// <returns></returns>
        private static async Task ReceiveFeedback(
            ServiceClient serviceClient) {

            var feedbackReceiver = serviceClient.GetFeedbackReceiver();

            while (true) {

                // ReceiveAsync returns as soon as there is a message
                // to read from the hub or after a timeout expires.
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();

                if (feedbackBatch == null) {
                    // the timeout has expired there are no 
                    // feedback messages from the device
                    continue;
                }

                foreach (var record in feedbackBatch.Records) {

                    var messageId = record.OriginalMessageId;

                    // Success
                    // Expired
                    // DeliveryCountExceeded
                    // Rejected
                    // Purged
                    var statusCode = record.StatusCode;

                    // is a full manager it would handle each case 
                    // accordingly i.e. retry, send message to admin,
                    // notify the device with a warning message, etc.

                    Console.WriteLine($"Feedback for message '{messageId}', status code: {statusCode}.");

                }

                // this tells the Hub that we are done with this 
                // batch of feedback messages from the devices and
                // can get on with the next on the next iteration
                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }
    }
}
