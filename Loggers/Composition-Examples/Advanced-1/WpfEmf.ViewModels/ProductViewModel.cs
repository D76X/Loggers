using System.ComponentModel.Composition;
using WpfEmf.Interfaces;

namespace WpfEmf.ViewModels {
    [Export(typeof(ProductViewModel))]
    public class ProductViewModel: WorkSpaceViewModel {        

        public ProductViewModel() {
            this.HeaderText = "ProductViewModel";
        }

        private int productCounter;

        public int ProductCounter {
            get { return productCounter; }
            set { SetProperty<int>(ref productCounter,value,nameof(ProductCounter)); }
        }
    }
}
