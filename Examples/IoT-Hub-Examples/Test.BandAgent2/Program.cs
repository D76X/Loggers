using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

namespace Test.BandAgent1 {
    class Program {

        private const string DeviceConnectionString = @"HostName=psdemohub1.azure-devices.net;DeviceId=device-01;SharedAccessKey=RF82V2O/5usaLo01FQFTMYHZoDtLYRTU/FxUDv9Nm8A=";

        static async Task Main(string[] args) {

            Console.WriteLine("Initializing Band Agent...");

            var device = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

            await device.OpenAsync();

            Console.WriteLine("The device is connected!");

            // here we work with the twins of a device
            // we update the reported properties of the device

            // a twin collection is a dictionary
            // property names for a twin are restricted use pascal case
            // do not use spaces, periods, dollar signs or control chars
            // the hub will reject malformed property names
            // the hub silently rejects the entire update
            var twinProperties = new TwinCollection();
            twinProperties["connectionType"] = "wi-fi";
            twinProperties["connectionStrenght"] = "weak";

            await device.UpdateReportedPropertiesAsync(twinProperties);

            Console.WriteLine("sent update to device twin properties");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
