using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Unity;
using LogXtreme.WinDsk.TestDocking.Prism.Interfaces;
using LogXtreme.WinDsk.TestDocking.Prism.ViewModels;
using LogXtreme.WinDsk.TestDocking.Prism.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.TestDocking.Prism.Modules {

    [Module(ModuleName = nameof(PlottingModule))]
    public class PlottingModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public PlottingModule(
            IUnityContainer container,
            RegionManager regionManager) {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IPlottingViewModel, PlottingViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            // regionManager.RegisterViewWithRegion(RegionNames.RegionPlotting, typeof(PlottingView));

            // Register the View for navigation only
            this.container.RegisterTypeForNavigation<PlottingView>();
        }
    }
}