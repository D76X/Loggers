using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Services;
using MainMenuModule.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace MainMenuOptionsModule {

    [ModuleDependency(nameof(MainMenuModule))]
    public class MainMenuOptionsModule : IModule {

        private readonly IMenuService menuService;

        public MainMenuOptionsModule(IMenuService menuService) {
            this.menuService = menuService;
        }

        public void Initialize() {

            // Register types

            // Subscribe to Services or Events

            // Register Shared Services

            // Compose Views into the Shell

            //...
            this.menuService.AddTopLevelMenu(new MenuItemNode() { Text = "Options"});
        }
    }
}
