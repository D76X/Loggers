using LogXtreme.WinDsk.DataGridModule.Interfaces;
using LogXtreme.WinDsk.DataGridModule.ViewModels;
using LogXtreme.WinDsk.DataGridModule.Views;
using LogXtreme.WinDsk.Infrastructure.Services;
using LogXtreme.WinDsk.Infrastructure.Unity;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace LogXtreme.WinDsk.Modules {

    [Module(ModuleName = nameof(DataGridModule))]
    [ModuleDependency(nameof(MainMenuModule))]    
    public class DataGridModule : IModule {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public DataGridModule(
            IUnityContainer container,
            RegionManager regionManager,
            IMenuService menuService) {

            this.container = container;
            this.regionManager = regionManager;

            // register menu items 

            // register toolbar items
        }

        public void Initialize() {

            // Register types
            this.container.RegisterType<IDataGridViewModel, DataGridViewModel>();

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell
            // regionManager.RegisterViewWithRegion(RegionNames.RegionDockingManager, typeof(DataGridView));

            // Register the View for navigation only
            this.container.RegisterTypeForNavigation<DataGridView>();
        }
    }
}
