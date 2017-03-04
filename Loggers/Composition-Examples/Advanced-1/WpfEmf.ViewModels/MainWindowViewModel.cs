using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmf.Interfaces;

namespace WpfEmf.ViewModels {
    public class MainWindowViewModel : BindableBase {

        private WorkSpaceViewModel selectedViewModel = new ProductViewModel();

        public WorkSpaceViewModel SelecedViewModel {
            get {

                return this.selectedViewModel;
            }

            set {

                this.SetProperty<WorkSpaceViewModel>(ref this.selectedViewModel, value, nameof(SelecedViewModel));
            }
        }

        private ObservableCollection<WorkSpaceViewModel> workspaces;

        public ObservableCollection<WorkSpaceViewModel> Workspaces {

            get {
                if (this.workspaces == null) {
                    this.workspaces = new ObservableCollection<WorkSpaceViewModel>();
                }
                return this.workspaces;
            }

            set {


            }
        }
    }
}
