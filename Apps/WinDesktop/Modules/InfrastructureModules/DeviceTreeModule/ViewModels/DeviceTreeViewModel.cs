using DeviceTreeModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using Prism.Mvvm;
using System;

namespace DeviceTreeModule.ViewModels {

    public class DeviceTreeViewModel : 
        BindableBase,       
        IDeviceTreeViewModel, 
        IDisposable {

        private readonly IDeviceService deviceService;
        private TreeItemViewModel<DeviceModel> deviceTree;

        public DeviceTreeViewModel(IDeviceService deviceService) {

            this.deviceService = deviceService;
        }

        public TreeItemViewModel<DeviceModel> DeviceTree {

            get { return this.deviceTree; }

            set {

                SetProperty(ref this.deviceTree, value);
            }
        }

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {
                // dispose of subscriptions, etc.               
            }
        }

        #endregion
    }
}
