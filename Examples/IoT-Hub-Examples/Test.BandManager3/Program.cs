using Microsoft.Azure.Devices;
using System;
using System.Text;
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

            while (true) {

                Console.WriteLine("Which device do you wish to send a message to? ");
                Console.Write("> ");
                var deviceId = Console.ReadLine();

                // obviously this sends a message straight to the device
                await SendCloudToDeviceMessage(serviceClient, deviceId);                
            }
        }

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
