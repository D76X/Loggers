using LogXtreme.WinDsk.Infrastructure.Menu;
using LogXtreme.WinDsk.Infrastructure.Models;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.MainMenuModule.ViewModels {

    public class MenuItemViewModel : 
        ViewModelBase, 
        IMenuItem {

        private string header;
        private IMenuItem parent;
        private IEnumerable<IMenuItem> children;

        public MenuItemViewModel(
            string header,
            IMenuItem parent,
            IEnumerable<IMenuItem> children) {

            this.header = header;
            this.parent = parent;

            if (children != null) {
                foreach (var child in children) {

                    var childViewModel = (child as MenuItemViewModel);

                    if (childViewModel != null) {
                        childViewModel.Parent = parent;
                    }
                }
            }

            this.children = children;
        }

        public string Header {
            get { return this.header; }
            set { this.SetProperty(ref this.header, value); }
        }

        public IMenuItem Parent {
            get { return this.parent; }
            set { this.SetProperty(ref this.parent, value); }
        }

        public IEnumerable<IMenuItem> Children => this.children;
    }
}
