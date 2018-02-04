using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Services;
using MainMenuModule.ViewModels;
using Prism.Modularity;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.Modules {

    [Module(ModuleName = nameof(MainMenuOptionsModule))]
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
            IMenuItem parent = null;
            List<IMenuItem> children = null;
            this.menuService.AddMenuItem(new MenuItemViewModel("Options", parent, children));
        }
    }
}
