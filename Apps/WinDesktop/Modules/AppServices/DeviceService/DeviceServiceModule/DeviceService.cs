using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Services;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.Modules.Services {

    public class DeviceService : IDeviceService {

        public IEnumerable<DeviceModel> GetDevices() {

            return new List<DeviceModel>() {
                new DeviceModel(){Name=@"device1"},
                new DeviceModel(){Name=@"device2"}
            };
        }
    }
}
