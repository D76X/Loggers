using IntroMEF.Interfaces;
using System.ComponentModel.Composition;

namespace Modules {

    [Export(typeof(IModule))]
    internal class ModuleProduct: IModule {

        public ModuleProduct() {
            Title = "Product";
        }

        public string Title { get; set; }
    }
}