using DeviceTreeModule.Interfaces;
using Prism.Mvvm;
using System;

namespace DeviceTreeModule.ViewModels {
    public class DeviceTreeViewModel : BindableBase, IDeviceTreeViewModel, IDisposable {

        public DeviceTreeViewModel() {

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
