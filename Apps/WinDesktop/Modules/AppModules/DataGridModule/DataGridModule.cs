using Prism.Modularity;
using LogXtreme.WinDsk.Infrastructure.Menu;
using Prism.Regions;
using Microsoft.Practices.Unity;
using LogXtreme.WinDsk.Infrastructure.Services;
using DataGridModule.Interfaces;
using LogXtreme.WinDsk.Infrastructure;

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
            regionManager.RegisterViewWithRegion(RegionNames.WorkspaceTabControl, typeof(DataGridView));
        }
    }
}
