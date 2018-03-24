using LogXtreme.Ifrastructure.Enums;
using LogXtreme.Ifrastructure.Interfaces;
using LogXtreme.Infrastructure.ContractValidators;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    public class DeviceEventArgs : EventArgs {

        public readonly IDevice Device;
        public readonly DeviceEventEnum EventType;

        public DeviceEventArgs(
            IDevice device,
            DeviceEventEnum eventType) {

            device.Validate(nameof(device)).NotNull();
            eventType.Validate(nameof(eventType)).NotNull();

            this.Device = device;
            this.EventType = eventType;
        }
    }
}
