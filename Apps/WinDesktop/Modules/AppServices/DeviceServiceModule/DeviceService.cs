using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Services;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.Modules.Services {

    public class DeviceService : IDeviceService {

        public IEnumerable<DeviceModel> GetDevices() {

            return new List<DeviceModel>() {

                new DeviceModel(){
                    Name =@"device1",
                    Address=@"add1",
                    Port=@"port1",
                    Staus=@"active",
                    Type="USB"
                },

                new DeviceModel(){
                    Name =@"device2",
                    Address=@"add2",
                    Port=@"port2",
                    Staus=@"active",
                    Type="ETHERNET"
                }
            };
        }
    }
}
