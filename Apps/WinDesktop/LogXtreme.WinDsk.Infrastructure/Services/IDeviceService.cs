using LogXtreme.WinDsk.Infrastructure.Models;

namespace LogXtreme.WinDsk.Infrastructure.Services {

    public interface IDeviceService {

        Node<DeviceModel> GetDevices();
    }
}
