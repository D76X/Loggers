using System;
using System.Threading.Tasks;

namespace Test.BandManager2
{
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
    class Program
    {
        private const string ServiceConnectionString = @"HostName=psdemohub1.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=7nTmG5YbixIy8iNhcrSRyCJURUIS6zsgIViSyj6JjDM=";

        static async Task Main(string[] args) {

            //
        }
    }
}
