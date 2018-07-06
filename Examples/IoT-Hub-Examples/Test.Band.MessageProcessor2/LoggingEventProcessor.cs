using Globomantics.Common;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Band.MessageProcessor2 {

    class LoggingEventProcessor : IEventProcessor {

        /// <summary>
        /// Called when the processor is attached to a new partition for a Hub.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OpenAsync(PartitionContext context) {
            Console.WriteLine("LoggingEventProcessor opened, processing partition: " +
                              $"'{context.PartitionId}'");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the processor is detached from a partition for a Hub.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public Task CloseAsync(PartitionContext context, CloseReason reason) {
            Console.WriteLine("LoggingEventProcessor closing, partition: " +
                              $"'{context.PartitionId}', reason: '{reason}'.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called any time something goes wrong with the processor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Task ProcessErrorAsync(PartitionContext context, Exception error) {
            Console.WriteLine("LoggingEventProcessor error, partition: " +
                              $"{context.PartitionId}, error: {error.Message}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// As data reaches the IoT Hub it will be passed to this method for 
        /// processing.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messages">a batch of messages from the Hub</param>
        /// <returns></returns>
        public Task ProcessEventsAsync(
            PartitionContext context, 
            IEnumerable<EventData> messages) {

            Console.WriteLine($"Batch of events received on partition '{context.PartitionId}'.");

            // go through the batch of messages to process
            foreach (var eventData in messages) {

                // extract the payload that was sent by the the device up to the 
                // hub as part of the message. Remember that the device can push 
                // any serialized object up to the IoT Hub as byte array!
                var payload = Encoding.ASCII.GetString(eventData.Body.Array,
                    eventData.Body.Offset,
                    eventData.Body.Count);

                // the event as other information attached to it that can 
                // be also used by the processor logic. Some info mayh be 
                // about the Hub and other baout the device that sent the 
                // message to it.
                var deviceId = eventData.SystemProperties["iothub-connection-device-id"];

                Console.WriteLine($"Message received on partition '{context.PartitionId}', " +
                                  $"device ID: '{deviceId}', " +
                                  $"payload: '{payload}'");

                // Here it is now possible to extract the payload as a byte array
                // and deserialize it into a POCO object, then do with it all you
                // like.

                var telemetry = JsonConvert.DeserializeObject<Telemetry>(payload);

                // For example if the user of the device has pushed the emergency button 
                // you may want something to happen such as messages should be sent to 
                // a resque unit or similar...

                if (telemetry.Status == StatusType.Emergency) {
                    Console.WriteLine($"Guest requires emergency assistance! Device ID: {deviceId}");
                    SendFirstRespondersTo(telemetry.Latitude, telemetry.Longitude);
                }
            }

            // this call is important because it causes the processor to write a 
            // checkpoint to the checkpoint blob storage. The processor uses this 
            // blob storage to keept track of the messages on th eIoT Hub that it
            // has already processed. Without this call the processor would reprocess
            // all the messages on the IoT Hub each time it runs.
            return context.CheckpointAsync();
        }

        private void SendFirstRespondersTo(decimal latitude, decimal longitude) {
            //In a real app, this is where we would send a command or notification!
            Console.WriteLine($"**First responders dispatched to ({latitude}, {longitude})!**");
        }
    }
}
