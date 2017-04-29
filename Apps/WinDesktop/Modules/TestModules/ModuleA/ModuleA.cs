using System;
using Prism.Modularity;
using Prism.Regions;
using LogXtreme.WinDsk.Infrastructure;
using ModuleA.Views;
using Microsoft.Practices.Unity;

namespace LogXtreme.WinDsk.Modules.TestModules.ModuleA {
    public class TestModuleA : IModule {

        private RegionManager regionManager;
        private IUnityContainer container;

        public TestModuleA(
            IUnityContainer unityContainer,
            RegionManager regionManager) {

            this.regionManager = regionManager;
            this.container = unityContainer;
        }

        public void Initialize() {

            // Register types

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell

            IRegion toolbarRegion = this.regionManager.Regions[RegionNames.RegionToolbar];
            toolbarRegion.Add(this.container.Resolve<ToolbarView>());
            toolbarRegion.Add(this.container.Resolve<ToolbarView>());

            regionManager.RegisterViewWithRegion(RegionNames.RegionContent, typeof(ContentView));
        }
    }
}
