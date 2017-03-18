using WpfMef.Interfaces;
using System.ComponentModel.Composition;
using System.Windows;

namespace WpfMef.Modules {

    [Export(typeof(IModule))]
    internal class ModuleCustomer: IModule {

        public ModuleCustomer() {
            Title = "Customer";
        }

        public string Title { get; set; }

        public UIElement Content
        {
            get { return new CustomerList(); }
        }
    }
}