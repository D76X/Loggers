using System;
using System.Collections.Generic;
using LogXtreme.WinDsk.Infrastructure.Menu;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace MainMenuModule.ViewModels {

    public class MenuItemViewModel : BindableBase, IMenuItem {

        private string header;
        private IMenuItem parent;
        private IEnumerable<IMenuItem> children;

        public MenuItemViewModel(
            string header,
            IEnumerable<IMenuItem> children) {

            this.header = header;
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
