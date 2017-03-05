using System.ComponentModel.Composition;
using WpfEmf.Interfaces;

namespace WpfEmf.ViewModels {
    [Export(typeof(ProductViewModel))]
    public class ProductViewModel : WorkSpaceViewModel {

        public readonly RelayCommand increaseProductCountCommand;

        public ProductViewModel():base("ProductWorkSpace") {

            this.increaseProductCountCommand = new RelayCommand(
                this.onExecuteIncreaseCounter,
                this.canExecuteIncreaseCounter
                );
        }

        private int productCounter;

        public int ProductCounter {
            get { return productCounter; }
            private set { SetProperty<int>(ref productCounter, value, nameof(ProductCounter)); }
        }        

        public RelayCommand IncreaseProductCountCommand {
            get {
                return this.increaseProductCountCommand;
            }           
        }

        private void onExecuteIncreaseCounter() {
            this.ProductCounter += 1;
        }

        private bool canExecuteIncreaseCounter() {
            return true;
        }
    }
}
