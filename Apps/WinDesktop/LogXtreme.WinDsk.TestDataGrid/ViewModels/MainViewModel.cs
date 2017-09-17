using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class MainViewModel {

        ObservableCollection<object> viewModels;

        public MainViewModel() {
            this.viewModels = new ObservableCollection<object>();
            this.viewModels.Add(new DataGrid1ViewModel());
            this.viewModels.Add(new DataGrid2ViewModel());
        }

        ObservableCollection<object> ViewModels { get { return this.viewModels;  } }
    }
}
