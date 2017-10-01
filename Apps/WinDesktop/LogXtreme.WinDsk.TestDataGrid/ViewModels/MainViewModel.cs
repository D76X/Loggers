using LogXtreme.WinDsk.TestDataGrid.Utils;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class MainViewModel {

        ObservableCollection<object> viewModels;

        public MainViewModel() {

            this.viewModels = new ObservableCollection<object>();
            this.viewModels.Add(new DataGrid1ViewModel());
            this.viewModels.Add(new DataGrid2ViewModel());
            this.viewModels.Add(new DataSourceViewModel(new DataSource()));
        }

        public ObservableCollection<object> ViewModels {
            get {
                return this.viewModels;
            }
        }
    }
}
