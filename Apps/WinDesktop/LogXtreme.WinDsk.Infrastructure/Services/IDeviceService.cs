using LogXtreme.WinDsk.Infrastructure.Models;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    public interface IDeviceService {

        IEnumerable<DeviceModel> GetDevices();
    }
}
