using System.ComponentModel.Composition;
using WpfEmf.Interfaces;

namespace WpfEmf.ViewModels {
    [Export(typeof(ProductViewModel))]
    public class ProductViewModel: WorkSpaceViewModel {

        public ProductViewModel() {
            this.HeaderText = "ProductViewModel";
        }
    }
}
