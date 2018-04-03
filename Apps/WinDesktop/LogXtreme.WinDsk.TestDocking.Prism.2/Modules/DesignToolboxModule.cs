using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Unity;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using LogXtreme.WinDsk.TestDocking.Prism.ViewModels;
using LogXtreme.WinDsk.TestDocking.Prism.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Modules {

    [Module(ModuleName = nameof(DesignToolboxModule))]
    public class DesignToolboxModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public DesignToolboxModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }
        public void Initialize() {

            // Register types
            this.container.RegisterType<IDesignToolboxViewModel, DesignToolboxViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            //regionManager.RegisterViewWithRegion(RegionNames.RegionDataTree, typeof(DesignToolboxView));

            // Register the View for navigation
            this.container.RegisterTypeForNavigation<DesignToolboxView>();
        }
    }
}
