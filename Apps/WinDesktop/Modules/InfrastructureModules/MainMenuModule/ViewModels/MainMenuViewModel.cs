using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.ReactiveExtensions;
using LogXtreme.WinDsk.Infrastructure.Services;
using MainMenuModule.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace MainMenuModule.ViewModels {

    public class MainMenuViewModel : BindableBase, IMainMenuViewModel, IDisposable {

        private readonly IMenuService menuService;

        private ObservableCollection<IMenuItem> menuItems;

        private IDisposable subscriptionToAddMenuItem;

        public MainMenuViewModel(IMenuService menuService) {

            this.menuService = menuService;

            this.subscriptionToAddMenuItem = Observable.FromEventPattern<MenuItemEventArgs>(
                h => this.menuService.AddMenuItemEvent += h,
                h => this.menuService.AddMenuItemEvent -= h)
                .SubscribeWeakly(eventPattern => {
                    this.AddMenuItemEventHanlder(eventPattern.Sender, eventPattern.EventArgs);
                });

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

        #region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposing) {

                if (this.subscriptionToAddMenuItem != null) {

                    this.subscriptionToAddMenuItem.Dispose();
                    this.subscriptionToAddMenuItem = null;
                }
            }
        }

        #endregion
    }
}
