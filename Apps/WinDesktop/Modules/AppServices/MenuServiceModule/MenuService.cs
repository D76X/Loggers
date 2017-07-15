using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Services;
using System;

namespace LogXtreme.WinDsk.Modules.Services {

    public class MenuService : IMenuService {

        public void AddMenuItem(IMenuItem menuItem) {

            this.AddMenuItemEvent?.Invoke(null, new MenuItemEventArgs(menuItem));
        }

        public event EventHandler<MenuItemEventArgs> AddMenuItemEvent;

        public IMenuItem GetMenuItem(string header) {
            throw new NotImplementedException();
        }    
    }
}
