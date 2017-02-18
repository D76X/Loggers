using WpfMef.Interfaces;
using System.ComponentModel.Composition;
using System.Windows;

namespace WpfMef.Modules {

    [Export(typeof(IModule))]
    internal class ModuleProduct: IModule {

        public ModuleProduct() {
            Title = "Product";
        }

        public string Title { get; set; }

        public UIElement Content
        {
            get { return new ProductList(); }
        }
    }
}