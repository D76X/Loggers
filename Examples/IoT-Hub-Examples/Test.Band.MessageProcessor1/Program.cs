using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Threading.Tasks;

namespace Test.Band.MessageProcessor1 {

    /// <summary>
    /// This AP.NET Core app is used to connect up to an IoT Hub and 
    /// process the messages on it. It relies on the basic implementation 
    /// provided in the NuGet pkg Microsoft.Azure.EventHubs.Processor.
    /// </summary>
    class Program {
        static async Task Main(string[] args) {

            // from the IoT Hub account on the Azure portal.
            // hub info
            var hubName = "psdemohub1";
            var iotHubConnectionString = "Endpoint=sb://iothub-ns-psdemohub1-515804-aaa147083f.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=UDr/oJdwKflSnYaM/mQu4FjHZWE2y7AzzRKwPp1a28s=";

            // This message processor makes use of Azure Blob Storage
            // to store information about the progress it makes while 
            // processing the messages on the hub. In particular the 
            // processor stores checkpoints.

            // When you created the IoT Hub you also created a storage
            // account to store the messages sent by the device app to
            // the IoT Hub on a blob storage.

            // The checkpoints messages used by this message processor
            // can be stored in the same way as the messages for the 
            // IoT hub. However, you must use a dedicated blob storage.
            // If not already available create this new blob storage 
            // under the same storage account for the blob storage used
            // to save the IoT Hub messages.

            // from the Storage account on the Azure portal find the 
            // Access keys for the storage account you will need the 
            // connection string of the storage account and you also 
            // must know the name of teh blob stogare that you want 
            // to store the checkpoints on. 

            // the two pieces of data below.
            // storage info
            var storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=dsdemostorage01;AccountKey=YNFfJ8pXjiRKbQb5c0NU/v57kniW5dfg7rGNqJdlbJ0a8KsdDV+0OeESbXfoxsg7l2PbO6fDzQzw06zqzvB88w==;EndpointSuffix=core.windows.net";
            var storageContainerName = "messages-processor-host";

            // You need to know the name of teh Consumer Group you want to 
            // attache the host processor to. Remember that a Consumer Group
            // is a view on the IoT Hub...
            // In our case we are using the default Consumer Group but there
            // may be more i.e. custom Consumer Groups that you have created.
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;

            // Now we have all the info we need to create the Processor Host.
            var processor = new EventProcessorHost(
                hubName,
                consumerGroupName,
                iotHubConnectionString,
                storageConnectionString,
                storageContainerName);

            // before you can use the event processor host that is this application
            // you must register an event processor with the IoT Hub.
            await processor.RegisterEventProcessorAsync<LoggingEventProcessor>();

            Console.WriteLine("Event processor started, press enter to exit...");
            Console.ReadKey();

            // remember to unregister teh event processor
            await processor.UnregisterEventProcessorAsync();
        }
    }
}
