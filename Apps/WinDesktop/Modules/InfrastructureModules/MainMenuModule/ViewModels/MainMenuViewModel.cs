using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Services;
using MainMenuModule.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace MainMenuModule.ViewModels {

    public class MainMenuViewModel : IMainMenuViewModel {

        private readonly IMenuService menuService;
        private ObservableCollection<IMenuItem> menuItems;

        public MainMenuViewModel(IMenuService menuService) {

            this.menuService = menuService;
            this.menuService.AddMenuItemEvent += this.AddMenuItemEventHanlder;

            var menuItems = new List<IMenuItem>();  
            
            var newMenuItem = new MenuItemViewModel("_New", null, null);
            var openMenuItem = new MenuItemViewModel("_Open", null, null);

            var fileMenuItemChildren = new List<IMenuItem>() {
                newMenuItem,
                openMenuItem,
            };

            var fileMenuItem = new MenuItemViewModel("_File", null, fileMenuItemChildren);

            menuItems.Add(fileMenuItem);

            this.menuItems = new ObservableCollection<IMenuItem>(menuItems);
        }

        private void AddMenuItemEventHanlder(object sender, MenuItemEventArgs e) {
            this.menuItems.Add(e.MenuItem);
        }

        public ObservableCollection<IMenuItem> MenuItems => this.menuItems;

        
    }
}
