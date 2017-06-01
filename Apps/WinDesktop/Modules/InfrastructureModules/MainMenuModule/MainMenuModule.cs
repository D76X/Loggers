using LogXtreme.WinDsk.Infrastructure;
using LogXtreme.WinDsk.Infrastructure.Services;
using MainMenuModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace MainMenuModule
{
    [Module(ModuleName = nameof(MainMenuModule))]
    public class MainMenuModule : IModule
    {

        private readonly IUnityContainer container;
        private readonly RegionManager regionManager;

        public MainMenuModule(
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
            this.container.RegisterType<IMenuService, MenuService.MenuService>(new ContainerControlledLifetimeManager());

            // Compose Views into the Shell
            regionManager.RegisterViewWithRegion(RegionNames.RegionMainMenu, typeof(MainMenuView));
        }
    }
}
