using System;
using LogXtreme.WinDsk.Infrastructure.Menu;
using Prism.Mvvm;

namespace MainMenuModule.ViewModels {

    public class MenuItemNode : BindableBase, IMenuItemNode {

        public string text;

        public string Text {
            get { return this.text; }
            set { this.SetProperty(ref this.text, value); }
        }
    }
}
