using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Unity;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using LogXtreme.WinDsk.TestDocking.Prism.ViewModels;
using LogXtreme.WinDsk.TestDocking.Prism.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Modules {

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

            // Compose the Views directly into the Shell targeting an existing named Prism region.
            regionManager.RegisterViewWithRegion(RegionNames.RegionDeviceTree, typeof(DeviceTreeView));
            
            // Register the View for navigation
            this.container.RegisterTypeForNavigation<DeviceTreeView>();
        }
    }
}
