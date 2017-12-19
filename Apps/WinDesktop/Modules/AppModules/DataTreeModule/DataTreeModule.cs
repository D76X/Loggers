using DataTreeModule.Interfaces;
using DataTreeModule.ViewModels;
using DataTreeModule.Views;
using LogXtreme.WinDsk.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.Modules {

    [Module(ModuleName = nameof(DataTreeModule))]
    public class DataTreeModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public DataTreeModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }


        public void Initialize() {

            // Register types
            this.container.RegisterType<IDataTreeViewModel, DataTreeViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionRightPanels, typeof(DataTreeView));
        }
    }
}
