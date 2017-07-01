﻿using DeviceTreeModule.Interfaces;
using DeviceTreeModule.ViewModels;
using DeviceTreeModule.Views;
using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DeviceTreeModule {

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
            regionManager.RegisterViewWithRegion(RegionNames.RegionDeviceTree, typeof(DeviceTreeView));
        }
    }
}
