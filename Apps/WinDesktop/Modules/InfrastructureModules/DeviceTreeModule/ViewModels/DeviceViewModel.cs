using LogXtreme.WinDsk.Infrastructure.Models;
using Prism.Mvvm;

namespace DeviceTreeModule.ViewModels {

    public class DeviceViewModel : BindableBase {

        private DeviceModel deviceModel;

        public DeviceViewModel(DeviceModel deviceModel) {

            this.deviceModel = deviceModel;
        }

        public string Name => this.deviceModel.Name;

        public string Type => this.deviceModel.Type;

        public string Address => this.deviceModel.Address;

        public string Port => this.deviceModel.Port;

        public string Staus => this.deviceModel.Staus;
    }
}
