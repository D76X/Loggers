using IntroMEF.Interfaces;
using System.ComponentModel.Composition;

namespace Modules {

    [Export(typeof(IModule))]
    internal class ModuleSupplier: IModule {

        public ModuleSupplier() {
            Title = "Supplier";
        }

        public string Title { get; set; }
    }
}