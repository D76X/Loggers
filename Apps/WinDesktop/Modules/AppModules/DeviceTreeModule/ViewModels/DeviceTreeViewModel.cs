using LogXtreme.WinDsk.DeviceTreeModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure.Models;
using LogXtreme.WinDsk.Infrastructure.Services;
using Prism.Regions;
using System;
using System.Linq;

namespace LogXtreme.WinDsk.DeviceTreeModule.ViewModels {

    public class DeviceTreeViewModel :
        ViewModelBase,
        IDeviceTreeViewModel,
        IDisposable {

        private readonly IDeviceService deviceService;
        private TreeItemViewModel<DeviceViewModel> deviceTree;

        public DeviceTreeViewModel(IDeviceService deviceService) {

            this.deviceService = deviceService;

            var devices = deviceService.GetDevices();

            var master = new DeviceModel() { Name = @"master" };
            var root = new DeviceViewModel(master);
            var deviceViewModels = devices.Select(d => new DeviceViewModel(d));

            var rootNode = new Node<DeviceViewModel>(root, null, null);
            var childNodes = deviceViewModels.Select(dvm =>
                new Node<DeviceViewModel>(dvm, rootNode, null));

            childNodes.ToList().ForEach(cn => rootNode.Add(cn));

            this.deviceTree = new TreeItemViewModel<DeviceViewModel>(rootNode, null);
        }

        public TreeItemViewModel<DeviceViewModel> DeviceTree {
            get => this.deviceTree;
            set => SetProperty(ref this.deviceTree, value);
        }
        
        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            base.OnNavigatedFrom(navigationContext);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            base.OnNavigatedTo(navigationContext);
            base.RaiseNavigatedTo();
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
