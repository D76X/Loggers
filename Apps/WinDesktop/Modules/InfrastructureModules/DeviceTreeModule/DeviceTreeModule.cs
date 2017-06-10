using System;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Regions;
using LogXtreme.WinDsk.Infrastructure;

namespace DeviceTreeModule {

    [Module(ModuleName = nameof(DeviceTreeModule))]
    public class DeviceTreeModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public DeviceTreeModule(
            IUnityContainer container,
            RegionManager regionManager
            ) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionDeviceTree, typeof(MainMenuView));
        }
    }
}
