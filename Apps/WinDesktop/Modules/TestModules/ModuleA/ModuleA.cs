using System;
using Prism.Modularity;
using Prism.Regions;
using LogXtreme.WinDsk.Infrastructure;

namespace LogXtreme.WinDsk.Modules.TestModules {
    public class TestModuleA : IModule {

        private RegionManager regionManager;

        public TestModuleA(RegionManager regionManager) {
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionToolbar, typeof(ToolbarView));
            regionManager.RegisterViewWithRegion(RegionNames.RegionContent, typeof(ContentView));
        }
    }
}
