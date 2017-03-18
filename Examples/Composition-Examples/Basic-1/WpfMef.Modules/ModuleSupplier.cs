using WpfMef.Interfaces;
using System.ComponentModel.Composition;
using System.Windows;

namespace WpfMef.Modules {

    [Export(typeof(IModule))]
    internal class ModuleSupplier: IModule {

        public ModuleSupplier() {
            Title = "Supplier";
        }

        public string Title { get; set; }

        public UIElement Content
        {
            get { return new SupplierList(); }
        }
    }
}