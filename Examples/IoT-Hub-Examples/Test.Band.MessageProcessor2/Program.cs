using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using System;
using System.Threading.Tasks;

namespace Test.Band.MessageProcessor2 {

    /// <summary>
    /// This AP.NET Core app is used to connect up to an IoT Hub and 
    /// process the messages on it. It relies on the basic implementation 
    /// provided in the NuGet pkg Microsoft.Azure.EventHubs.Processor.
    /// 
    /// This is the same as MessageProcessor1 but the the Main method is 
    /// not async. This is necessary to deploy this processor app to Azure 
    /// as a WebJob.
    /// </summary>
    class Program {
        static void Main(string[] args) {

            var hubName = "psdemohub1";
            var iotHubConnectionString = "Endpoint=sb://iothub-ns-psdemohub1-515804-aaa147083f.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=UDr/oJdwKflSnYaM/mQu4FjHZWE2y7AzzRKwPp1a28s=";
                      
            var storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=dsdemostorage01;AccountKey=YNFfJ8pXjiRKbQb5c0NU/v57kniW5dfg7rGNqJdlbJ0a8KsdDV+0OeESbXfoxsg7l2PbO6fDzQzw06zqzvB88w==;EndpointSuffix=core.windows.net";
            var storageContainerName = "messages-processor-host";

            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;

            // Now we have all the info we need to create the Processor Host.
            var processor = new EventProcessorHost(
                hubName,
                consumerGroupName,
                iotHubConnectionString,
                storageConnectionString,
                storageContainerName);

            processor.RegisterEventProcessorAsync<LoggingEventProcessor>().Wait();

            //-----------------------------------------------------------------------
            // this is code specific to the deployment of this processor app as
            // a WebJob on the Azure infrastructure. This makes use of the NuGet
            // packages (both in beta for the .NET Core)
            // Microsoft.Azure.WebJobs
            // Microsoft.Azure.WebJobs.Extensions.EventHubs

            // 1- add the processor to the hub
            var eventHubConfig = new EventHubConfiguration();
            eventHubConfig.AddEventProcessorHost(hubName, processor);

            // 2- create a host for the WebJob
            var jobHostConfig = new JobHostConfiguration(storageConnectionString);

            // 3- wire the processor on the hub to the WebJob
            jobHostConfig.UseEventHub(eventHubConfig);

            //4- create and start a Job Host
            var host = new JobHost(jobHostConfig);

            //5- run the job
            Console.WriteLine("starting event processor on Job Host as WebJob...");
            host.RunAndBlock();
            //-----------------------------------------------------------------------
        }
    }
}
