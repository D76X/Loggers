using LogXtreme.WinDsk.Infrastructure.Menu;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Services {
    public interface IMenuService {
        void AddMenuItem(IMenuItem menuItem);
        event EventHandler<MenuItemEventArgs> AddMenuItemEvent;
    }
}
