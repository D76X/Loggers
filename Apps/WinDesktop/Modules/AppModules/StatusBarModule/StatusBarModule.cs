using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.StatusBarModule.Interfaces;
using LogXtreme.WinDsk.StatusBarModule.ViewModels;
using LogXtreme.WinDsk.StatusBarModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.Modules {

    [Module(ModuleName = nameof(StatusBarModule))]
    public class StatusBarModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public StatusBarModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IStatusBarViewModel, StatusBarViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionStatusBar, typeof(StatusBarView));
        }
    }
}
