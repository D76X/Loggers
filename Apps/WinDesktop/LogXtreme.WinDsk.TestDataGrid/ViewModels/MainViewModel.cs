using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class MainViewModel {

        ObservableCollection<object> viewModels;

        public MainViewModel() {
            this.viewModels = new ObservableCollection<object>();
            this.viewModels.Add(new DataGrid1ViewModel());
            this.viewModels.Add(new DataGrid2ViewModel());
        }

        public ObservableCollection<object> ViewModels {
            get {
                return this.viewModels;
            }
        }
    }
}
