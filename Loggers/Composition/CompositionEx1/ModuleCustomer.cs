using System.ComponentModel.Composition;

namespace Modules {

    [Export(typeof(IModule))]
    internal class ModuleCustomer: IModule {

        public ModuleCustomer() {
            Title = "Customer";
        }

        public string Title { get; set; }
    }
}