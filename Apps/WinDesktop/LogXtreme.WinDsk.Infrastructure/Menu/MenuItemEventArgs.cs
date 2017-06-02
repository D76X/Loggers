using System;

namespace LogXtreme.WinDsk.Infrastructure.Menu {
    public class MenuItemEventArgs : EventArgs {

        public readonly IMenuItem MenuItem;

        public MenuItemEventArgs(IMenuItem menuItem) {
            this.MenuItem = menuItem;
        }
    }
}
