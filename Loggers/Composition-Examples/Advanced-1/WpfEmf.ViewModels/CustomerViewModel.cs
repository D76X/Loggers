using System.ComponentModel.Composition;
using WpfEmf.Interfaces;

namespace WpfEmf.ViewModels {

    [Export(typeof(CustomerViewModel))]
    public class CustomerViewModel : WorkSpaceViewModel {

        public CustomerViewModel() : base("CustomerWorkSpace") { }

    }
}
