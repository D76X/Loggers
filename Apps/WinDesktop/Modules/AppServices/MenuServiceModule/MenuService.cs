using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Services;
using System;

namespace LogXtreme.WinDsk.Modules.Services {

    public class MenuService : IMenuService {

        public void AddMenuItem(IMenuItem menuItem) {

            this.OnMenuItemAdded?.Invoke(null, new MenuItemEventArgs(menuItem));
        }

        public event EventHandler<MenuItemEventArgs> OnMenuItemAdded;

        public IMenuItem GetMenuItem(string header) {
            //TODO Implement MenuService.GetMenuItem
            throw new NotImplementedException();
        }    
    }
}
