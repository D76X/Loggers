using LogXtreme.WinDsk.TestDataGrid.Models;
using LogXtreme.WinDsk.TestDataGrid.Utils;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.TestDataGrid.ViewModels {
    public class MainViewModel {

        ObservableCollection<object> viewModels;

        public MainViewModel() {

            this.viewModels = new ObservableCollection<object>();
            this.viewModels.Add(new DataGrid1ViewModel());
            this.viewModels.Add(new DataGrid2ViewModel());

            var sampleDescriptor1 = new SampleDescriptor(new string[] { "CHN0", "CHN1", "CHN2" });
            var dataSource1 = new DataSource1(sampleDescriptor1, new RandomGenerator());
            this.viewModels.Add(new DataSourceViewModel1(dataSource1));

            //this.viewModels.Add(new DataSourceViewModel2(new DataSource2()));
        }

        public ObservableCollection<object> ViewModels {
            get {
                return this.viewModels;
            }
        }
    }
}
