using LogXtreme.WinDsk.DeviceTreeModule.Interfaces;
using LogXtreme.WinDsk.DeviceTreeModule.ViewModels;
using LogXtreme.WinDsk.DeviceTreeModule.Views;
using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Unity;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.Modules {

    [Module(ModuleName = nameof(DeviceTreeModule))]
    public class DeviceTreeModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public DeviceTreeModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IDeviceTreeViewModel, DeviceTreeViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            this.regionManager.RegisterViewWithRegion(RegionNames.RegionDockingManager, typeof(DeviceTreeView));

            // Register the View for navigation
            this.container.RegisterTypeForNavigation<DeviceTreeView>();
        }
    }
}
