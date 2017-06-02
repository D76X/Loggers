using LogXtreme.WinDsk.Infrastructure.Menu;
using MainMenuModule.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MainMenuModule.ViewModels {
    public class MainMenuViewModel : IMainMenuViewModel {

        private ObservableCollection<IMenuItem> menuItems;

        public MainMenuViewModel() {

            var menuItems = new List<IMenuItem>();

            //< !--< Menu >
            //< MenuItem Header = "_File" >

            //     < MenuItem Header = "_New" />

            //      < MenuItem Header = "_Open" />

            //       < MenuItem Header = "_Save" />

            //        < Separator />

            //        < MenuItem Header = "_Exit" />

            //     </ MenuItem >

            // </ Menu > -->           

            var newMenuItem = new MenuItemViewModel("_New", null);
            var openMenuItem = new MenuItemViewModel("_Open", null);
            var fileMenuItemChildren = new List<IMenuItem>() {
                newMenuItem,
                openMenuItem,
            };

            menuItems.Add(new MenuItemViewModel("_File", fileMenuItemChildren));

            this.menuItems = new ObservableCollection<IMenuItem>(menuItems);
        }

        public ObservableCollection<IMenuItem> MenuItems => this.menuItems;
    }
}
