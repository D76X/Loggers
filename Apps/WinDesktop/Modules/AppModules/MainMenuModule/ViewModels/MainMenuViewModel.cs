using LogXtreme.Reactive.Extensions;
using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Services;
using MainMenuModule.Interfaces;
using MainMenuModule.Labels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace MainMenuModule.ViewModels {

    public class MainMenuViewModel :
        BindableBase,
        IMainMenuViewModel,
        IDisposable {

        private readonly IMenuService menuService;

        private ObservableCollection<IMenuItem> menuItems;

        private IDisposable eventsubscription_AddMenuItemObservable;

        public MainMenuViewModel(IMenuService menuService) {

            this.menuService = menuService;           

            this.eventsubscription_AddMenuItemObservable = Observable
                .FromEventPattern<MenuItemEventArgs>(
                h => this.menuService.OnMenuItemAdded += h,
                h => this.menuService.OnMenuItemAdded -= h)
                .SubscribeWeakly(
                this,
                (target, ep) => target.AddMenuItemEventHanlder(ep.Sender, ep.EventArgs));

            var menuItems = new List<IMenuItem>();

            var fileMenuItems = this.CreateFileMenuItems();
            menuItems.Add(fileMenuItems);

            var editMenuItems = this.CreateEditMenuItems();
            menuItems.Add(editMenuItems);

            this.menuItems = new ObservableCollection<IMenuItem>(menuItems);
        }

        public ObservableCollection<IMenuItem> MenuItems => this.menuItems;

        private void AddMenuItemEventHanlder(object sender, MenuItemEventArgs e) {
            this.menuItems.Add(e.MenuItem);
        }

        private MenuItemViewModel CreateFileMenuItems() {

            var newMenuItem = new MenuItemViewModel(FileMenuLabels.New, null, null);
            var openMenuItem = new MenuItemViewModel(FileMenuLabels.Open, null, null);

            var fileMenuItemChildren = new List<IMenuItem>() {
                newMenuItem,
                openMenuItem,
            };

            var fileMenuItem = new MenuItemViewModel(FileMenuLabels.File, null, fileMenuItemChildren);

            return fileMenuItem;
        }

        private MenuItemViewModel CreateEditMenuItems() {

            var doMenuItem = new MenuItemViewModel(EditMenuLabels.Do, null, null);
            var undoMenuItem = new MenuItemViewModel(EditMenuLabels.Undo, null, null);

            var fileMenuItemChildren = new List<IMenuItem>() {
                doMenuItem,
                undoMenuItem,
            };

            var editMenuItem = new MenuItemViewModel(EditMenuLabels.Edit, null, fileMenuItemChildren);

            return editMenuItem;
        }

        #region IDisposable

        private bool disposedValue = false; // To detect redundant calls         

        protected virtual void Dispose(bool disposing) {

            if (!disposedValue) {

                if (disposing) {

                    this.eventsubscription_AddMenuItemObservable?.Dispose();
                    this.eventsubscription_AddMenuItemObservable = null;
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose() {
            this.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        #endregion
    }
}
