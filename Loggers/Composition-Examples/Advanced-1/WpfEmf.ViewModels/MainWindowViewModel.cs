using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfEmf.Interfaces;

namespace WpfEmf.ViewModels {
    public class MainWindowViewModel : BindableBase {

        public MainWindowViewModel(IEnumerable<WorkSpaceViewModel> viewModels) {

            this.workSpaces = new ObservableCollection<WorkSpaceViewModel>();
            viewModels.ToList().ForEach(vm => this.workSpaces.Add(vm));
            this.selectedViewModel = this.workSpaces.FirstOrDefault();
           
        }

        private WorkSpaceViewModel selectedViewModel;

        public WorkSpaceViewModel SelecedViewModel {
            get {

                return this.selectedViewModel;
            }

            set {

                this.SetProperty<WorkSpaceViewModel>(ref this.selectedViewModel, value, nameof(SelecedViewModel));
            }
        }

        private ObservableCollection<WorkSpaceViewModel> workSpaces;

        public ObservableCollection<WorkSpaceViewModel> WorkSpaces {

            get {
                return this.workSpaces;
            }
        }
    }
}
